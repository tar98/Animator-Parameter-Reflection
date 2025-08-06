#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimatorParameterCreation : EditorWindow {
	public List<AnimatorController> animatorControllers;
	private SerializedObject so;
	private const string Title = "Create Reflection File";
	private const int BUFFER_SIZE = 8192;

	private void OnEnable() {
		string[] paths = Directory.GetFiles("Assets", "*.controller", SearchOption.AllDirectories);
		object[] data = Array.ConvertAll<string, object>(paths, input => AssetDatabase.LoadAssetAtPath(input, typeof(AnimatorController)));
		animatorControllers = new List<AnimatorController>(Array.ConvertAll(data, input => input as AnimatorController));
		so = new SerializedObject(this);
	}

	private void OnDisable() {
		EditorUtility.ClearProgressBar();
		so.Dispose();
	}

	private void OnGUI() {
		so.Update();
		
		SerializedProperty property = so.FindProperty("animatorControllers");
		EditorGUILayout.PropertyField(
			property,
			new GUIContent("Animator Controller", EditorGUIUtility.IconContent("d_NetworkAnimator Icon").image)
		);
		
		so.ApplyModifiedProperties();

		bool disable =
			animatorControllers.Count == 0 ||
			animatorControllers.Exists(x => x == null) ||
			animatorControllers.Count != animatorControllers.Distinct().Count();
		
		if (disable) HelpBox();
		
		EditorGUI.BeginDisabledGroup(disable);
		if (GUILayout.Button("Create Reflection", GUI.skin.button)) {
			EditorUtility.ClearProgressBar();
			_ = Reflection(
				EditorUtility.SaveFilePanel(
					"Save file",
					"Assets", 
					"AnimatorParameter",
					"cs"
				)
			);
		}
		EditorGUI.EndDisabledGroup();
	}
	
	[MenuItem("Tool/Animator Parameter Creation")]
	public static void OpenMenu() => GetWindow<AnimatorParameterCreation>("Animator Parameter Creation");

	private void HelpBox() {
		if (animatorControllers.Count == 0) {
			EditorGUILayout.HelpBox("The List is Empty!", MessageType.Error, true);
			return;
		}
		
		if (animatorControllers.Exists(x => x == null)) {
			EditorGUILayout.HelpBox("There is a null reference inside the List!", MessageType.Warning, true);
		}

		if (animatorControllers.Count != animatorControllers.Distinct().Count()) {
			EditorGUILayout.HelpBox("There is duplicate reference inside the List!", MessageType.Warning, true);
		}
	}

	private async Task Reflection(string path) {
		if (string.IsNullOrEmpty(path)) return;
		
		// Get the path file.
		string fileName = Path.GetFileNameWithoutExtension(path);
		await using (FileStream stream = File.Create(path, BUFFER_SIZE, FileOptions.Asynchronous)) {
			const string initInfo = "Initialize...";
			
			// Create initial comment
			await WriteFile(stream, PreGenerateComment(), initInfo, 0f);
			Thread.Sleep(100);
			
			// Create the usings
			await WriteFile(stream, "using UnityEngine;\n\n", initInfo, 0.5f);
			Thread.Sleep(100);
			
			// Create the abstract class using the same name of the file.
			await WriteFile(stream, SetClassDeclaration(fileName), initInfo, 1f);
			Thread.Sleep(100);
			
			// Create each parameter
			for (int i = 0; i < animatorControllers.Count; i++) {
				// Create each controller class by name
				string info = $"Create Parameters : {animatorControllers[i].name} ";
				
				await WriteFile(stream, SetSubClassDeclaration(animatorControllers[i].name), info, 0f);
				Thread.Sleep(250);
				
				AnimatorControllerParameter[] parameters = animatorControllers[i].parameters;
				
				// Create each controller parameter const by name
				for (int j = 0; j < parameters.Length; j++) {
					await WriteFile(stream, SetParamDeclaration(parameters[j].name), info, (float)j / parameters.Length);
					Thread.Sleep(250);
				}
				
				// Close the class controller subclass
				await WriteFile(stream, CloseSubClassBrace(), info, 1f);
				Thread.Sleep(250);
			}
			
			// Close the file and save it
			await WriteFile(stream, CloseClassBrace(), "Close and save file...", 0f);
			Thread.Sleep(200);
			EditorUtility.DisplayProgressBar(Title, "Close and save file...", 1f);
			Thread.Sleep(100);
			
			stream.Close();
			// no need of "stream.DisposeAsync()" because once the using is closed it'll be called automatically.
		}
		
		EditorUtility.ClearProgressBar();
		EditorUtility.DisplayDialog(Title, $"Create Animator Reflection \nPath : {Path.GetRelativePath("Assets", path)}", "Okay");
		AssetDatabase.Refresh();
	}

#region REFECTION_FUNCTION

	private async Task WriteFile(FileStream file, string data, string progressInfo, float progress) {
		byte[] bytes = GetTextToByte(data);
		EditorUtility.DisplayProgressBar(Title, progressInfo, progress);
		await file.WriteAsync(bytes, 0, bytes.Length);
	}

	private string SetClassDeclaration(string text) {
		string str = ReplaceNotValidChar(new StringBuilder(text));
		return $"public abstract class {str} " + "{ \n";
	}
	
	private string SetSubClassDeclaration(string text) {
		string str = ReplaceNotValidChar(new StringBuilder(text));
		return $"\tpublic abstract class {str} " + "{ \n";
	}
	
	private string SetParamDeclaration(string text) {
		string str = ReplaceNotValidChar(new StringBuilder(text));
		return $"\t\tpublic static readonly int {str} = Animator.StringToHash(nameof({str}));\n";
	}

	private string CloseClassBrace() => "}\n";
	
	private string CloseSubClassBrace() => "\t}\n\n";
	
	private byte[] GetTextToByte(string text) => Encoding.Default.GetBytes(text);
	
	private string ReplaceNotValidChar(StringBuilder text) {
		const char separator = '_';
		
		text = text.Replace(' ', separator);
		text = text.Replace('-', separator);
		text = text.Replace('@', separator);
		text = text.Replace('[', separator);
		text = text.Replace(']', separator);
		text = text.Replace('(', separator);
		text = text.Replace(')', separator);
		return text.ToString();
	}
    
	private string PreGenerateComment() {
		return "/////////////////////////////////////////////////////////////////\n" +
		       "//// \tThis class is generated by the Animator Parameter Reflection\n" +
		       "/////////////////////////////////////////////////////////////////\n";
	}
}
#endregion
#endif
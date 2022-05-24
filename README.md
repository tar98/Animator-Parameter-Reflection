# Animator Parameter Reflection
A Unity Editor Tool that create a C# script using a Reflection with in input a list of Animator Controller

___

### Conflict
I had a issues when i work with **Animator Controller**.
In code i usually save the the hash's parameter inside the same class where i have the **Animator Component**, but sometime i need that hash
outside of that class, and i am constrained to make a public variable or a property.
<br>Another issue is when other my colleague create a
new **Animator Controller** without caching the hash's property give me a lot of waste time.

___

### Objective

Create a unique class that contain all the hash's parameter spit up for they Controller.

___

## How to Use

Use the tool is quite simple, as long as you have the Editor Script _'AnimatorParameterCreation.cs'_.

### Open

Let's start with shortcut, above on the Navigation Board e follow the path **'Tool/Animator Parameter Creation'**

![MediaImage](Media/Shortcut.jpg)

___

> 🛈 _You can change the path inside the script 'AnimatorParameterCreation.cs' if you want..._

___

### Editor Window

When the Editor Window is open it automatically search all Animator Controllers inside the Assets Folder and SubFolders.
After the search create a list of Animation Controller so you can immediately start the Reflection.

![MediaImage](Media/EditorWindow.jpg)

___

> ⚠️ _<span style="color:yellow">Warning!</span> If you modify the list there are 3 condition you have tu fulfill before you click!_
> 1. _You cannot have **Null Reference** in the list._
> 2. _You cannot have **Duplicate** instance in the list._
> 3. _The list cannot be **Empty**._

___

### Save File and Start Reflection

You now click the Button **'Reflection'** and it will open a Save Dialog Panel, when you save the file in the directory you choose it begin the reflection.
<br> It will appear a Progress Bar that indicate the state of the file, at the end of procedure it show a Message Box Content the relative path of the file.

![MediaImage](Media/Reflection.gif)

### File

This is the Generate File at the end of the Reflection.

![MediaImage](Media/ReflectionFile.jpg)
 
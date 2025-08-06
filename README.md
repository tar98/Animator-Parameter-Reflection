# Animator Parameter Reflection
A Unity Editor Tool that creates a C# script using Reflection with as input a list of Animator Controllers.

___

### Conflict
I had issues when I worked with **Animator Controller**.
In code, I usually save the hash's parameter inside the same class where I have the **Animator Component**, but sometimes I need that hash
outside of that class, and I am obligated to make a public variable or a property.
<br>Another issue is when my colleague creates a new **Animator Controller** without caching the hash's property gives me a lot of wasted time finding out the parameter's name.

___

### Objective

Create a unique class that contains all the hash's parameters, split up for the Controller.

___

## How to Use

Using the tool is quite simple, as long as you have the Editor Script _'AnimatorParameterCreation.cs'_.

### Open

Let's start with a shortcut, above on the Navigation Board, and follow the path **'Tool/Animator Parameter Creation'**

![MediaImage](Media/Shortcut.jpg)

___

> 🛈 _You can change the path inside the script 'AnimatorParameterCreation.cs' if you want..._

___

### Editor Window

When the Editor Window is open, it automatically searches all Animator Controllers inside the Assets Folder and SubFolders.
After the search, create a list of *Animation Controller* so you can immediately start the Reflection.

![MediaImage](Media/EditorWindow.jpg)

___

> ⚠️ _<span style="color:yellow">Warning!</span> If you modify the list, there are 3 conditions you have to fulfill before you click!_
> 1. _You cannot have **Null Reference** in the list._
> 2. _You cannot have **Duplicate** instance in the list._
> 3. _The list cannot be **Empty**._

___

### Save File and Start Reflection

You now click the Button **'Reflection'** and it will open a Save Dialogue Panel. When you save the file in the chosen directory, the reflection process begins.
<br> A progress bar will appear to indicate the state of the file, at the end of the procedure, and it shows a Message Box containing the relative path of the file.

![MediaImage](Media/Reflection.gif)

### File

This is the Generate File at the end of the Reflection.

![MediaImage](Media/ReflectionFile.jpg)
 

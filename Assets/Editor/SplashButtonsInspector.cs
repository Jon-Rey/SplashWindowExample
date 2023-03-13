using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

/// <summary>
/// This script was made by Jonathan Reynolds for Example Purposes. 
/// Feel Free to edit this as needed but keep this Summary reference.
/// </summary>


[CustomEditor(typeof(ExampleNamespace.SplashScreen.SplashButtons))]
public class SplashButtonsInspector : Editor
{


    public bool NamesFoldout = false;
    public bool ButtonsFoldout = false;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var SceneFolder = serializedObject.FindProperty("SceneFolder");
        EditorGUILayout.PropertyField(SceneFolder);
        EditorGUILayout.Separator();
        HandleSceneNames();
        EditorGUILayout.Separator();
        HandleButtons();



        serializedObject.ApplyModifiedProperties();
    }

    public void HandleButtons()
    {
        var Buttons = serializedObject.FindProperty("Buttons");
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.BeginHorizontal();
        ButtonsFoldout = EditorGUILayout.Foldout(ButtonsFoldout, new GUIContent($"{Buttons.arraySize} Splash Buttons: "));
        if (GUILayout.Button(new GUIContent("Add New Button")))
        {
            var splashButtons = serializedObject.targetObject as ExampleNamespace.SplashScreen.SplashButtons;

            var button = new ExampleNamespace.SplashScreen.SplashButton("New Button", "");
            splashButtons.Buttons.Add(button);
            ButtonsFoldout = true;
        }
        EditorGUILayout.EndHorizontal();

        
        if (Buttons.arraySize > 0)
        {
            EditorGUI.indentLevel += 1;
            if (ButtonsFoldout)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                for (int i = 0; i < Buttons.arraySize; i++)
                {
                    var button = Buttons.GetArrayElementAtIndex(i);
                    DrawButton(Buttons, button as SerializedProperty, i);
                    GUILayout.Space(25);
                }
                EditorGUILayout.EndVertical();
            }
        }
        
        
    }

    public void DrawButton(SerializedProperty buttons, SerializedProperty button, int index)
    {
        var allbuttons = target as ExampleNamespace.SplashScreen.SplashButtons;
        var thisButton = allbuttons.Buttons[index];

        var bText = button.FindPropertyRelative("ButtonText");
        var bType = button.FindPropertyRelative("ButtonType");
        var link = button.FindPropertyRelative("Link");
        var image = button.FindPropertyRelative("Image");


        EditorGUILayout.PropertyField(bType);
        EditorGUILayout.PropertyField(bText);

        switch(thisButton.ButtonType)
        {
            case ExampleNamespace.SplashScreen.SplashButton.bType.WebLink:
                EditorGUILayout.PropertyField(link);
                break;
            case ExampleNamespace.SplashScreen.SplashButton.bType.File:
                EditorGUILayout.PropertyField(link, new GUIContent("File Path"));
                break;
        }
        EditorGUILayout.PropertyField(image);

        UIArrayHandler(buttons, index);
    }

    public void HandleSceneNames()
    {
        var SceneNames = serializedObject.FindProperty("SceneNames");
        EditorGUILayout.BeginHorizontal();
        NamesFoldout = EditorGUILayout.Foldout(NamesFoldout, new GUIContent($"{SceneNames.arraySize} Scene Names: "));
        if (GUILayout.Button(new GUIContent("Add New Scene")))
        {
            var splashButtons = serializedObject.targetObject as ExampleNamespace.SplashScreen.SplashButtons;

            var scene = new ExampleNamespace.SplashScreen.SplashSceneName(SceneManager.GetActiveScene().name);

            splashButtons.SceneNames.Add(scene);
            NamesFoldout = true;
        }
        EditorGUILayout.EndHorizontal();

        if (SceneNames.arraySize > 0)
        {
            EditorGUI.indentLevel += 1;
            if (NamesFoldout)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);
                for (int i = 0; i < SceneNames.arraySize; i++)
                {
                    var name = SceneNames.GetArrayElementAtIndex(i);
                    DrawName(SceneNames, name as SerializedProperty, i);
                    GUILayout.Space(25);
                }
                EditorGUILayout.EndVertical();
            }
        }
        
       
    }

    public void DrawName(SerializedProperty names, SerializedProperty _name, int index)
    {
        var name = _name.FindPropertyRelative("Name");
        var image = _name.FindPropertyRelative("Image");

        EditorGUILayout.PropertyField(name);
        EditorGUILayout.PropertyField(image);

        UIArrayHandler(names, index);
    }



    public void UIArrayHandler(SerializedProperty serArray, int itemIdx)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Up", GUILayout.Width(64)))
        {
            if (itemIdx > 0)
                serArray.MoveArrayElement(itemIdx, itemIdx - 1);
        }
        if (GUILayout.Button("Down", GUILayout.Width(64)))
        {
            if (itemIdx < serArray.arraySize - 1)
                serArray.MoveArrayElement(itemIdx, itemIdx + 1);
        }
        if (GUILayout.Button("Delete", GUILayout.Width(64)))
        {
            serArray.DeleteArrayElementAtIndex(itemIdx);
            return;
        }
        GUILayout.EndHorizontal();
    }
}

using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using UnityEditor;
using UnityEngine;
using static JonReyn.Tools.EditorTools;

[CustomEditor(typeof(ClassExample))]
public class ClassExampleInspector : Editor
{
    private bool exampleFoldout = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var CE = target as ClassExample;

        exampleFoldout = ClassExampleGUI(CE, exampleFoldout);

        serializedObject.ApplyModifiedProperties();
    }

    public static bool ClassExampleGUI(ClassExample exampleClass, bool foldout)
    {
        //Indent this line to line up all variables and to demo how to use indentLevel to align elements
        EditorGUI.indentLevel++;

        
        //Begin by saving off current foldout value and generating local Serialized object.
        //This allows "ClassExampleGUI" to be reused in a static context
        bool return_foldout = foldout;
        SerializedObject serializedObject = new SerializedObject(exampleClass);
        serializedObject.Update();

        
        //Demo of how to use InclusiveFields Function
        //============================================
        List<string> InclusiveFieldsList = new List<string>
        {
            nameof(ClassExample.CustomString),
            nameof(ClassExample.FieldInt),
        };

        List<string> DisabledFields = new List<string>
        {
            nameof(ClassExample.FieldInt),
        };

        EditorGUILayout.LabelField("Inclusive fields only show fields included in the list");
        InclusiveFields(exampleClass, serializedObject, InclusiveFieldsList, DisabledFields);
        //============================================
        
        
        EditorGUILayout.Space(20);
        
        
        
        //Demo of how to use ExclusiveFields Function
        //============================================
        List<string> ExclusiveFieldsList = new List<string>
        {
            nameof(ClassExample.FieldFloat),
        };
        
        return_foldout = EditorGUILayout.Foldout(foldout, "ExampleFoldout");
        if (foldout)
        {
            EditorGUILayout.LabelField("Exclusive Show ALL fields except the ones in the list");
            ExclusiveFields(exampleClass, serializedObject, ExclusiveFieldsList, DisabledFields);
        }
        //===========================================

        
        //Ensure to return indent level to original, either by saving and restoring it here
        //or subtract the number of incraments you did. 
        EditorGUI.indentLevel--;
        
        //Finalize by applying the modified properties.
        serializedObject.ApplyModifiedProperties();
        
        //And return the foldout value
        return return_foldout;
    }
    
}

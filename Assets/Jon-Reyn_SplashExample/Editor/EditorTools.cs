using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace JonReyn.Tools
{
    public static class EditorTools
    {
        public static void InclusiveFields<T>(T customClass, SerializedObject so, List<string> fields,
            List<string> disabledFields = null)
        {
            foreach (var classField in customClass.GetType().GetFields().ToList())
            {
                if (fields.Contains(classField.Name))
                {
                    if(disabledFields != null)
                        GUI.enabled = !disabledFields.Contains(classField.Name);
                    SerializedProperty serializedProperty = so.FindProperty(classField.Name);
                    EditorGUILayout.PropertyField(serializedProperty, new GUIContent(classField.Name));
                    GUI.enabled = true;
                }
            }
        }
        
        public static void ExclusiveFields<T>(T customClass, SerializedObject so, List<string> fields,
            List<string> disabledFields = null)
        {
            foreach (var classField in customClass.GetType().GetFields().ToList())
            {
                if (!fields.Contains(classField.Name))
                {
                    if(disabledFields != null)
                        GUI.enabled = !disabledFields.Contains(classField.Name);
                    SerializedProperty serializedProperty = so.FindProperty(classField.Name);
                    EditorGUILayout.PropertyField(serializedProperty, new GUIContent(classField.Name));
                    GUI.enabled = true;
                }
            }
        }

        /// <summary>
        /// Given a list, this will show some list movement buttons up/down/remove that will modify a copy of the list then return it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="itemIdx"></param>
        /// <param name="destroyGameObj"></param>
        /// <returns></returns>
        public static T UIArrayHandler<T>(List<T> array, int itemIdx, bool destroyGameObj = false)
        {

            GUILayout.BeginHorizontal();


            T removeGo = default(T);

            T item = array[itemIdx];

            GUI.enabled = (itemIdx > 0);
            if (GUILayout.Button("Up", GUILayout.Width(64)))
            {
                if (itemIdx > 0)
                {
                    array.RemoveAt(itemIdx);
                    array.Insert(itemIdx - 1, item);
                }
            }
            GUI.enabled = true;


            GUI.enabled = (itemIdx < array.Count - 1);
            if (GUILayout.Button("Down", GUILayout.Width(64)))
            {
                if (itemIdx < array.Count - 1)
                {

                    array.RemoveAt(itemIdx);
                    array.Insert(itemIdx + 1, item);
                }
            }
            GUI.enabled = true;

            if (GUILayout.Button("Delete", GUILayout.Width(64)))
            {
                array.RemoveAt(itemIdx);
                removeGo = item;

            }
            GUILayout.EndHorizontal();

            return removeGo;
        }
        
        /// <summary>
        /// For usage with Serialized property arrays
        /// </summary>
        /// <param name="serArray"></param>
        /// <param name="itemIdx"></param>
        public static void UIArrayHandler(SerializedProperty serArray, int itemIdx)
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

}
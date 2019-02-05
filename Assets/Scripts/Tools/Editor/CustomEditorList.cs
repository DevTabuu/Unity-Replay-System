using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CustomEditorList {

    private static readonly GUIContent
        _deleteButton = new GUIContent("-", "Delete field"),
        _addButton = new GUIContent("Add", "Add Field");

    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.LabelField(list.displayName);
        EditorGUI.indentLevel++;
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            if (GUILayout.Button(_deleteButton, EditorStyles.miniButton, GUILayout.Width(15f)))
            {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(i);
                if (list.arraySize == oldSize)
                    list.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        if (GUILayout.Button(_addButton, EditorStyles.miniButton, GUILayout.Width(50f)))
        {
            int listSize = list.arraySize;
            list.arraySize += 1;

            if (list.arraySize > 1)
                list.DeleteArrayElementAtIndex(list.arraySize - 1);
        }
        GUILayout.EndHorizontal();
        EditorGUI.indentLevel--;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recorder))]
public class RecorderEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Recorder recorder = target as Recorder;

        serializedObject.Update();
        CustomEditorList.Show(serializedObject.FindProperty("_recorders"));
        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(20);

        GUILayout.Label("Replaying");
        if (GUILayout.Button("Replay Recorder Data"))
        {
            recorder.Replay();
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Pause"))
        {
            recorder.Pause();
        }
        else if (GUILayout.Button("Play"))
        {
            recorder.Play();
        }

        GUILayout.EndHorizontal();
    }
}

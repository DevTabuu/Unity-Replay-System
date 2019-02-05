using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class ReplayWindow : EditorWindow
{
    private static readonly GUIContent
        OPEN_BUTTON = new GUIContent("...", "Open a replay file."),
        SAVE_BUTTON = new GUIContent("Save", "Save a replay file."),
        RELOAD_BUTTON = new GUIContent("Reload", "Reload...");

    private static float _currentTick = 0f;
    private static List<RecorderData> _recorderData;
    private static string _fileName = "NONE";

    private static List<Recorder> _recorders;

    [MenuItem("Replay System/Replay Window")]
    public static void ShowWindow()
    {
        _recorders = new List<Recorder>();
        GetWindow<ReplayWindow>(false, "Replay", true);
    }

    private void OnGUI()
    {
        if (_recorders == null)
            LoadRecorders();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Open file: " + _fileName, GUILayout.Width(145f));
        if (GUILayout.Button(OPEN_BUTTON, GUILayout.Width(30f)))
        {
            LoadReplay(EditorUtility.OpenFilePanel("Open a replay file.", "", "rwn"));
        }
        if (GUILayout.Button(SAVE_BUTTON, GUILayout.Width(60f)))
        {
            LoadRecorders();
            SaveReplay(_recorderData, EditorUtility.SaveFilePanel("Save a replay file.", "", "ReplayFile", "rwn"));
        }
        GUILayout.EndHorizontal();
        _currentTick = EditorGUILayout.Slider("Time", _currentTick, 0, 1);

        if(GUILayout.Button(RELOAD_BUTTON, GUILayout.Width(100f)))
        {
            LoadRecorders();
        }
        GUILayout.Label("Found recorders: " + _recorders.Count, GUILayout.Width(145f));
    }

    private List<RecorderData> LoadReplay(string filePath)
    {
        string[] filePathParts = filePath.Split('/');
        _fileName = filePathParts[filePathParts.Length - 1];

        return new List<RecorderData>();
    }

    private bool SaveReplay(List<RecorderData> data, string filePath)
    {
        if (data.Count < 1)
            return false;

        XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
        StreamWriter stream = new StreamWriter(filePath);
        foreach (RecorderData recorderData in data)
        {
            xmlSerializer.Serialize(stream, recorderData);
        }

        stream.Close();

        return true;
    }

    private void LoadRecorders()
    {
        _recorders = new List<Recorder>(FindObjectsOfType<Recorder>());
        _recorderData = new List<RecorderData>();

        foreach(Recorder recorder in _recorders)
        {
            _recorderData.Add(recorder.GetData());
        }
    }
}

[System.Serializable]
public class Test
{
    public string _string;

    public Test()
    {
        _string = "";
    }

    public Test(string text)
    {
        _string = text;
    }
}

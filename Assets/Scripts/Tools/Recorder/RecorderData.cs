using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecorderData : ScriptableObject {

    private Dictionary<int, List<RecordableAction>> _actions;
    public Dictionary<int, List<RecordableAction>> Actions { get { return _actions; } }

    private void Awake()
    {
        _actions = new Dictionary<int, List<RecordableAction>>();
    }

    public void AddAction(int tick, RecordableAction action)
    {
        if (!_actions.ContainsKey(tick))
            _actions.Add(tick, new List<RecordableAction>());
        _actions[tick].Add(action);
    }

    public List<RecordableAction> GetActions(int tick)
    {
        if (!_actions.ContainsKey(tick))
            return null;
        else
            return _actions[tick];
    }

}

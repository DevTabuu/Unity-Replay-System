using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {

    [SerializeField, HideInInspector]
    private ActionRecorder[] _recorders;

    [SerializeField]
    private RecorderData _data;

    [SerializeField, Range(1, 128)]
    private int _ticksPerSecond;

    private float _tickTimer;
    private float _tickTime;

    private int _currentTick;

    private bool _recording;
    private bool _replaying;
    private bool _paused;

	private void Start () {
        _tickTime = 1 / _ticksPerSecond;
        _tickTimer = 0f;
        _currentTick = 0;

        _recording = true;
        _replaying = false;

        _data = ScriptableObject.CreateInstance<RecorderData>();

        foreach (ActionRecorder recorder in _recorders)
            recorder.SetTarget(gameObject);
    }

    private void FixedUpdate()
    {
        if (!_paused)
        {
            _tickTimer += Time.fixedDeltaTime;

            if (_tickTimer >= _tickTime)
            {
                if (_recording)
                    RecordTick();
                else if (_replaying)
                    ReplayTick();

                _tickTimer = 0f;
                _currentTick++;
            }
        }
    }

    private void RecordTick()
    {
        foreach (ActionRecorder recorder in _recorders)
        {
            RecordableAction action = recorder.Record();
            if (action != null)
                _data.AddAction(_currentTick, action);
        }
    }

    private void ReplayTick()
    {
        List<RecordableAction> actions = _data.GetActions(_currentTick);
        if(actions != null)
        {
            foreach (RecordableAction action in actions)
                action.Redo();
        }
    }

    public void Replay()
    {
        _replaying = true;
        _recording = false;
        _paused = false;

        for (int i = _currentTick; i >= 0; i--)
        {
            List<RecordableAction> actions = _data.GetActions(i);
            if(actions != null)
            {
                foreach (RecordableAction action in actions)
                    action.Undo();
            }
        }

        _currentTick = 0;
    }

    public void Pause()
    {
        _paused = true;
    }

    public void Play()
    {
        _paused = false;
    }

    public RecorderData GetData()
    {
        return _data;
    }
}

using System;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class ActionRecorder<T> : ActionRecorder where T : RecordableAction
{
    protected T _lastAction;
    public T GetLastRecordedAction()
    {
        return _lastAction;
    }
}

public abstract class ActionRecorder : ScriptableObject
{
    protected GameObject _target;

    public abstract RecordableAction Record();

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public GameObject GetTarget()
    {
        return _target;
    }
}

[System.Serializable]
public abstract class RecordableAction : ISerializable
{
    public abstract void Undo();

    public abstract void Redo();

    public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
}



using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Rotation Recorder", menuName = "Recorder/Recorders/Rotation Recorder", order = 1)]
public class RotationRecorder : ActionRecorder<RotationAction> {

    public override RecordableAction Record()
    {
        if (GetLastRecordedAction() != null && GetLastRecordedAction().GetTo().Equals(_target.transform.position))
            return null;

        RotationAction action = new RotationAction(_target, GetLastRecordedAction() == null ? _target.transform.rotation : GetLastRecordedAction().GetTo(), _target.transform.rotation);
        _lastAction = action;

        return action;
    }

}

public class RotationAction : RecordableAction
{
    GameObject _target;
    Quaternion _from, _to;

    public RotationAction(GameObject target, Quaternion from, Quaternion to)
    {
        _target = target;
        _from = from;
        _to = to;
    }

    public RotationAction(SerializationInfo info, StreamingContext context)
    {

    }

    public override void Redo()
    {
        _target.transform.rotation = _to;
    }

    public override void Undo()
    {
        _target.transform.rotation = _from;
    }

    public Quaternion GetFrom()
    {
        return _from;
    }

    public Quaternion GetTo()
    {
        return _to;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("GameObjectName", _target.name);
        info.AddValue("From", _from);
        info.AddValue("From", _to);
    }
}

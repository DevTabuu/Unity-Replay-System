using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Recorder", menuName = "Recorder/Recorders/Location Recorder", order = 1)]
public class LocationRecorder : ActionRecorder<LocationAction> {

    public override RecordableAction Record()
    {
        if (GetLastRecordedAction() != null && GetLastRecordedAction().GetTo().Equals(_target.transform.position))
            return null;

        LocationAction action = new LocationAction(_target, GetLastRecordedAction() == null ? _target.transform.position : GetLastRecordedAction().GetTo(), _target.transform.position);
        _lastAction = action;

        return action;
    }

}

public class LocationAction : RecordableAction
{
    GameObject _target;
    Vector3 _from, _to;

    public LocationAction(GameObject target, Vector3 from, Vector3 to)
    {
        _target = target;
        _from = from;
        _to = to;
    }

    public override void Redo()
    {
        _target.transform.position = _to;
    }

    public override void Undo()
    {
        _target.transform.position = _from;
    }

    public Vector3 GetFrom()
    {
        return _from;
    }

    public Vector3 GetTo()
    {
        return _to;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new System.NotImplementedException();
    }
}

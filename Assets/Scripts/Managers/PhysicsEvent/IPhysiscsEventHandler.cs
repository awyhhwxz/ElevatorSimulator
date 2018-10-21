using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhysicsEventType
{
    TriggerEnter,
    TriggerExit,
}

public interface IPhysiscsEventHandler {

    void Invoke(PhysicsEventType type, EventArgs args);
}

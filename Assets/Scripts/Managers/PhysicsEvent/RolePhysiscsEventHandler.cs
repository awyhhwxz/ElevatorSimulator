using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePhysiscsEventHandler : IPhysiscsEventHandler
{
    public void Invoke(PhysicsEventType type, EventArgs args)
    {
        var physicsArgs = (PhysiscsEventArgs)args;
        var collider = physicsArgs.TriggerCollider;
        if (collider.gameObject.layer == LayerTagConst.kRoleMoveAreaLayer)
        {
            if(collider.gameObject.CompareTag(LayerTagConst.kTakeElevatorAreaTagName))
            {
                HandleTakeElevator(type, physicsArgs);
            }
        }
    }

    private void HandleTakeElevator(PhysicsEventType type, PhysiscsEventArgs physicsArgs)
    {
        var roleObj = physicsArgs.RigidbodyObject;
        switch(type)
        {
            case PhysicsEventType.TriggerEnter:
                roleObj.transform.SetParent(physicsArgs.TriggerCollider.transform.parent);
                break;
            case PhysicsEventType.TriggerExit:
                roleObj.transform.SetParent(null);
                break;
        }
    }
}

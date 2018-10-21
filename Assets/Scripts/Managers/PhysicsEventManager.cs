using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEventManager : Singleton<PhysicsEventManager> {

    public enum HandlerType
    {
        Role,
    };

    private Dictionary<HandlerType, IPhysiscsEventHandler> _handlerDic = new Dictionary<HandlerType, IPhysiscsEventHandler>()
    {
        { HandlerType.Role, new RolePhysiscsEventHandler() },
    };

    public void InvokeEvent(HandlerType handlerType, PhysicsEventType eventType, EventArgs args)
    {
        IPhysiscsEventHandler eventHanlder = null;
        if(_handlerDic.TryGetValue(handlerType, out eventHanlder))
        {
            eventHanlder.Invoke(eventType, args);
        }
    }
}

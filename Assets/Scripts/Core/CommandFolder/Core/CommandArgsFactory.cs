using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandArgsFactory {

    protected Dictionary<int, Type> _creators;

    public EventArgs Create(int commandType)
    {
        Type type = null;
        if (_creators.TryGetValue(commandType, out type))
        {
            return Activator.CreateInstance(type, new object[] { }) as EventArgs;
        }

        return null;
    }
}

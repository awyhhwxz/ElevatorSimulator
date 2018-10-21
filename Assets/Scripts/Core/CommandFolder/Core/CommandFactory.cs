using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFactory {

    protected Dictionary<int, Type> _creators;
    
    public CommandContainer Create(int commandType, EventArgs args)
    {
        Type type = null;
        if (_creators.TryGetValue(commandType, out type))
        {
            return Activator.CreateInstance(type, new object[] { args }) as CommandContainer;
        }

        return null;
    }

    public EventArgs CreateArgs(int commandType)
    {
        return _argsFactory.Create(commandType);
    }

    protected CommandArgsFactory _argsFactory;
}

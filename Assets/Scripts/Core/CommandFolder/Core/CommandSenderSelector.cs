using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandSenderType
{
    CarCommand,
};

public class CommandSenderSelector : BaseSelector<ICommandSender> {
    
    public CommandSenderSelector()
    {
        _items = new Dictionary<int, ICommandSender>()
        {
            { (int)CommandSenderType.CarCommand, new CarCommandSender() },
        };
    }
}

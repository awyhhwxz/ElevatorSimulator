using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseCommandType
{
    CarRunCommand = 0,
    CarDoorOpenCommand,
}

public class CommandFactorySelector : BaseSelector<CommandFactory>
{

    public CommandFactorySelector()
    {
        _items = new Dictionary<int, CommandFactory>()
        {
            { (int)BaseCommandType.CarRunCommand, new CarRunCommandFactory() },
            { (int)BaseCommandType.CarDoorOpenCommand, new CarDoorOpenCommandFactory() }
        };
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarDoorOpenCommandFactory : CommandFactory
{
    public class ArgsFactory : CommandArgsFactory
    {
        public ArgsFactory()
        {
            _creators = new Dictionary<int, System.Type>()
            {
                { (int)CommandType.CopDoorButtonCast, typeof(CopDoorButtonCastArgs) },
            };
        }
    };

    public CarDoorOpenCommandFactory()
    {
        _creators = new Dictionary<int, System.Type>()
        {
            { (int)CommandType.CopDoorButtonCast, typeof(CarRunCommandCopDoorButtonCastContainer) }
        };

        _argsFactory = new ArgsFactory();
    }

    public enum CommandType
    {
        CopDoorButtonCast = 0,
    }
}

public class CopDoorButtonCastArgs : EventArgs
{
    public ButtonStateHandler StateHandler;
    public bool IsOpen;
}

public class CarRunCommandCopDoorButtonCastContainer : CommandContainer
{
    public CarRunCommandCopDoorButtonCastContainer(EventArgs args)
        : base(args)
    {
        CommandFactoryType = (int)CarDoorOpenCommandFactory.CommandType.CopDoorButtonCast;
        AssignArgs(args);
    }

    public override void AssignArgs(EventArgs args)
    {
        var copButtonCastArgs = args as CopDoorButtonCastArgs;
        if (copButtonCastArgs != null)
        {
            TargetCommand = new CarDoorOpenCommand()
            {
                IsOpen = copButtonCastArgs.IsOpen,
                OnEventAdd = () =>
                {
                    copButtonCastArgs.StateHandler.ActiveInAFlash();
                }
            };
        }
        else
        {
            Debug.LogErrorFormat("CarRunCommandCopDoorButtonCastContainer.AssignArgs.error : args type is invalid.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarRunCommandFactory : CommandFactory
{
    public class ArgsFactory : CommandArgsFactory
    {
        public ArgsFactory()
        {
            _creators = new Dictionary<int, System.Type>()
            {
                { (int)CommandType.CopNumButtonCast, typeof(OpNumButtonCastArgs) },
                { (int)CommandType.LopButtonCast, typeof(OpNumButtonCastArgs) },
            };
        }
    };

    public CarRunCommandFactory()
    {
        _creators = new Dictionary<int, System.Type>()
        {
            { (int)CommandType.CopNumButtonCast, typeof(CarRunCommandCopNumButtonCastContainer) },
            { (int)CommandType.LopButtonCast, typeof(CarRunCommandLopButtonCastContainer) },
        };

        _argsFactory = new ArgsFactory();
    }

    public enum CommandType
    {
        CopNumButtonCast = 0,
        LopButtonCast,
    }
}

public class OpNumButtonCastArgs : EventArgs
{
    public ButtonStateHandler StateHandler;
    public CarMoveManager.Direction TargetDirection = CarMoveManager.Direction.None;
    public int TargetLayer;
}

public class CarRunCommandCopNumButtonCastContainer : CommandContainer
{
    public CarRunCommandCopNumButtonCastContainer(EventArgs args)
        : base(args)
    {
        CommandFactoryType = (int)CarRunCommandFactory.CommandType.CopNumButtonCast;
        AssignArgs(args);
    }

    public override void AssignArgs(EventArgs args)
    {
        var copButtonCastArgs = args as OpNumButtonCastArgs;
        if(copButtonCastArgs != null)
        {
            TargetCommand = new CarRunCommand()
            {
                TargetLayer = copButtonCastArgs.TargetLayer,
                OnEventAdd = () =>
                {
                    copButtonCastArgs.StateHandler.SetButtonState(true);
                },
                OnEventEnd = () =>
                {
                    copButtonCastArgs.StateHandler.SetButtonState(false);
                    CarCommandReciever.Instance.SendCommand(new CarDoorOpenCommand() { IsOpen = true, ForceAdd = true });
                }
            };
        }
        else
        {
            Debug.LogErrorFormat("CarRunCommandCopButtonCastContainer.AssignArgs.error : args type is invalid.");
        }
    }
}

public class CarRunCommandLopButtonCastContainer : CommandContainer
{
    public CarRunCommandLopButtonCastContainer(EventArgs args)
        : base(args)
    {
        CommandFactoryType = (int)CarRunCommandFactory.CommandType.LopButtonCast;
        AssignArgs(args);
    }

    public override void AssignArgs(EventArgs args)
    {
        var opButtonCastArgs = args as OpNumButtonCastArgs;
        if (opButtonCastArgs != null)
        {
            TargetCommand = new CarRunCommand()
            {
                TargetLayer = opButtonCastArgs.TargetLayer,
                TargetDirection = opButtonCastArgs.TargetDirection,
                OnEventAdd = () =>
                {
                    opButtonCastArgs.StateHandler.SetButtonState(true);
                },
                OnEventEnd = () =>
                {
                    opButtonCastArgs.StateHandler.SetButtonState(false);
                    var trendRunCommand = new CarRunCommand();
                    trendRunCommand.CopyFrom(TargetCommand);
                    CarCommandReciever.Instance.SendCommand(new CarDoorOpenCommand() { Trend = trendRunCommand, IsOpen = true, ForceAdd = true });
                }
            };
        }
        else
        {
            Debug.LogErrorFormat("CarRunCommandCopButtonCastContainer.AssignArgs.error : args type is invalid.");
        }
    }
}

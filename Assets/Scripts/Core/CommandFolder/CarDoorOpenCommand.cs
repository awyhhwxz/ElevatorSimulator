using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorOpenCommand : Command
{
    public CarRunCommand Trend = null;

    public bool IsOpen;

    public bool ForceAdd = false;

    public override void CopyFrom(Command command)
    {
        var otherCommand = command as CarDoorOpenCommand;
        IsOpen = otherCommand.IsOpen;
    }

    public override bool Equals(object obj)
    {
        var isSameType = this.GetType().Equals(obj.GetType());
        if (isSameType)
        {
            var command = obj as CarDoorOpenCommand;
            return command.IsOpen == this.IsOpen;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.GetType().GetHashCode() ^ this.IsOpen.GetHashCode();
    }

    public CarDoorOpenCommand()
    {
        HandleEvent = HandleEventFunction;
        IsCompleted = IsCompletedFuntion;
        OnEventStart = OnEventStartFunction;
        AddCommand = AddCommandFunction;
        _carDoorAutoRunManager = CarDoorAutoRunManager.Instance;
    }

    private CarDoorAutoRunManager _carDoorAutoRunManager;

    private void OnEventStartFunction()
    {
        if(IsOpen)
        {
            _carDoorAutoRunManager.OpenDoor();
        }
        else
        {
            _carDoorAutoRunManager.CloseDoor();
        }
    }

    private bool IsCompletedFuntion()
    {
        return _carDoorAutoRunManager.CurrentDoorState == CarDoorAutoRunManager.DoorState.Closed;
    }

    private void HandleEventFunction()
    {
        _carDoorAutoRunManager.Update();
    }

    private void AddCommandFunction(ref Command currentCommand, ref List<Command> commandList)
    {
        bool NeedAdd = true;

        if (commandList.Count != 0)
        {
            var firstCommand = commandList[0];
            if (commandList[0].GetType().Equals(this.GetType()))
            {
                if(this.Trend == null)
                {
                    var oldCommand = commandList[0] as CarDoorOpenCommand;
                    this.Trend = oldCommand.Trend;
                }
                commandList[0] = this;
                NeedAdd = false;
            }
            else if(!ForceAdd)
            {
                NeedAdd = false;
            }
        }

        if(NeedAdd)
        {
            commandList.Insert(0, this);
        }
    }

    public override void Print()
    {
        Debug.LogErrorFormat("IsOpen:{0}", IsOpen);
    }

    public override int Priority { get { return 1; } }
}

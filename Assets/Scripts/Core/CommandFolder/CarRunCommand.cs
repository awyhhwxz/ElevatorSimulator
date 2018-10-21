using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CarRunCommand : Command
{
    public CarMoveManager.Direction TargetDirection = CarMoveManager.Direction.None;

    public int TargetLayer
    {
        get { return _targetLayer; }
        set
        {
            if(_targetLayer != value)
            {
                _targetLayer = value;
                _targetHeight = ElevatorConst.ParseLayerHeight(_targetLayer);
            }
        }
    }

    public override bool Equals(object obj)
    {
        var isSameType = this.GetType().Equals(obj.GetType());
        if (isSameType)
        {
            var command = obj as CarRunCommand;
            return command.TargetLayer == this.TargetLayer && command.TargetDirection == this.TargetDirection;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return this.GetType().GetHashCode() ^ this.TargetLayer.GetHashCode() ^ this.TargetDirection.GetHashCode();
    }

    public CarRunCommand()
    {
        HandleEvent = HandleEventFunction;
        IsCompleted = IsCompletedFuntion;
        OnEventStart = OnEventStartFunction;
        AddCommand = AddCommandFunction;
        _carAutoRunManager = CarAutoRunManager.Instance;
        _carMoveManager = CarMoveManager.Instance;
    }

    public override void Print()
    {
        Debug.LogErrorFormat("TargetLayer:{0}, Direction :{1}", TargetLayer, TargetDirection);
    }

    public override void CopyFrom(Command command)
    {
        var otherCommand = command as CarRunCommand;
        this.TargetLayer = otherCommand.TargetLayer;
        this.TargetDirection = otherCommand.TargetDirection;
    }

    public override int Priority { get { return 2; } }

    public override int SortCompareFunction(Command command1, Command command2)
    {
        var carRunCommand1 = command1 as CarRunCommand;
        var carRunCommand2 = command2 as CarRunCommand;
        if(carRunCommand1 != null && carRunCommand2 != null)
        {
            var relativeHeight1 = carRunCommand1.ParseRelativeHeight();
            //Debug.LogErrorFormat("Command: layer {0} direction {1}, height:{2}", carRunCommand1.TargetLayer, carRunCommand1.TargetDirection, relativeHeight1);
            var relativeHeight2 = carRunCommand2.ParseRelativeHeight();
            //Debug.LogErrorFormat("Command: layer {0} direction {1}, height:{2}", carRunCommand2.TargetLayer, carRunCommand2.TargetDirection, relativeHeight2);
            var different = relativeHeight1 - relativeHeight2;
            return Mathf.Equals(different, 0.0f) ? 0 : different > 0 ? 1 : -1;
        }
        else
        {
            return 0;
        }
    }
}

/// <summary>
/// private
/// </summary>
public partial class CarRunCommand : Command
{
    private int _targetLayer = 0;
    private float _targetHeight = 0;
    
    private CarAutoRunManager _carAutoRunManager;
    private CarMoveManager _carMoveManager;

    private void OnEventStartFunction()
    {
        _carAutoRunManager.RunToLayer(TargetLayer);
    }

    private bool IsCompletedFuntion()
    {
        return Mathf.Equals(_carMoveManager.GetCarHeight(), _targetHeight);
    }

    private void HandleEventFunction()
    {
        _carAutoRunManager.Update();
    }

    private float ParseRelativeHeight()
    {
        var targetHeight = ElevatorConst.ParseLayerHeight(this.TargetLayer);
        var currentHeight = CarMoveManager.Instance.GetCarHeight();
        
        float deltaHeight, addDeltaHeight, addDeltaRetraceHeight;
        var direction = CarCommandReciever.Instance.CarRunTrend;
        if (direction == CarMoveManager.Direction.Down)
        {
            deltaHeight = currentHeight - targetHeight;
            addDeltaHeight = ElevatorConst.ParseHeightLength(currentHeight);
            addDeltaRetraceHeight = ElevatorConst.ParseHeightLength(targetHeight);
        }
        else
        {
            deltaHeight = targetHeight - currentHeight;
            addDeltaHeight = ElevatorConst.ParseHeightComplementarySetLength(currentHeight);
            addDeltaRetraceHeight = ElevatorConst.ParseHeightComplementarySetLength(targetHeight);
        }

        float resultHeight = deltaHeight;
        if(deltaHeight < 0)
        {
            resultHeight = -deltaHeight + addDeltaHeight * 2;
        }

        //Debug.LogError(deltaHeight);
        //Debug.LogError("delta" + addDeltaRetraceHeight);
        if (TargetDirection != CarMoveManager.Direction.None && direction != CarMoveManager.Direction.None)
        {
            if (Mathf.Equals(deltaHeight, 0.0f))
            {
                if(direction != TargetDirection)
                {
                    resultHeight = addDeltaHeight * 2;
                }
            }
            else if(deltaHeight < 0)
            {
                if (direction == TargetDirection)
                {
                    resultHeight += (ElevatorConst.kElevatorPositionTotalHeight - addDeltaRetraceHeight) * 2;

                    //Debug.LogError("1:" + deltaHeight);
                }
            }
            else
            {
                if (direction != TargetDirection)
                {
                    resultHeight += addDeltaRetraceHeight * 2;
                    //Debug.LogError("2:" + deltaHeight);
                }
            }
        }

        return resultHeight;
    }

    private void AddCommandFunction(ref Command currentCommand, ref List<Command> commandList)
    {
        if (!commandList.Contains(this))
        {
            bool needAdd = true;

            if (commandList.Count == 0 || commandList[0].GetType().Equals(typeof(CarDoorOpenCommand)))
            {
                var relatveHeight = ParseRelativeHeight();
                if (Mathf.Equals(relatveHeight, 0.0f))
                {
                    if (this.OnEventEnd != null) this.OnEventEnd();
                    needAdd = false;
                }
            }

            if (needAdd)
            {
                commandList.Add(this);
            }
        }
    }
}
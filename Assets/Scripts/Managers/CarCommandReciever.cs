using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CarCommandReciever : Singleton<CarCommandReciever>
{
    private CarAutoRunManager _carAutoRunManger;

    private List<Command> _commandList = new List<Command>();
    private Command _currrentCommand = null;

    public CarCommandReciever()
    {
        _carAutoRunManger = CarAutoRunManager.Instance;
    }

    public override void Initialize()
    {
        base.Initialize();
        RefreshRunTrend();
    }

    public void SendCommand(Command command)
    {
        if(command != null)
        {
            AddCommondToList(command);
        }
    }

    public void Update()
    {
        if(_currrentCommand != null)
        {
            if(_currrentCommand.IsCompleted())
            {
                _commandList.Remove(_currrentCommand);
                if (_currrentCommand.OnEventEnd != null) _currrentCommand.OnEventEnd();
                TurnNextCommand();
            }
            else
            {
                if (_currrentCommand.HandleEvent != null) _currrentCommand.HandleEvent();
            }
        }
    }

    private void TurnNextCommand()
    {
        RefreshTrend();
        if (_commandList.Count != 0)
        {
            var firstCommand = _commandList[0];
            if(_currrentCommand != firstCommand)
            {
                _currrentCommand = firstCommand;
                if (_currrentCommand.OnEventStart != null) _currrentCommand.OnEventStart();
            }
        }
        else
        {
            _currrentCommand = null;
        }
    }

    public void HandleCarRun(CarRunCommand command)
    {
        command.OnEventAdd();
        _carAutoRunManger.RunToLayer(command.TargetLayer);
    }

    public CarMoveManager.Direction CarRunTrend { get { return _carRunTrend;} }
}

/// <summary>
/// Private
/// </summary>
public partial class CarCommandReciever : Singleton<CarCommandReciever>
{

    private void AddCommondToList(Command command)
    {
        if (command.OnEventAdd != null) command.OnEventAdd();
        if (command.AddCommand == null)
        {
            _commandList.Add(command);
        }
        else
        {
            command.AddCommand(ref _currrentCommand, ref _commandList);
        }
        
        _commandList.Sort(CommandCompare);
        //PrintCommandList();
        TurnNextCommand();
    }

    private void RefreshTrend()
    {
        //Debug.LogErrorFormat("Pre Trend is {0}", _CarRunTrend);
        _CarRunTrend = ParseRunDirction();
        //Debug.LogErrorFormat("Trend is {0}", _CarRunTrend);
    }

    private int CommandCompare(Command command1, Command command2)
    {
        var result = command1.Priority - command2.Priority;
        if (result == 0)
        {
            return command1.SortCompareFunction(command1, command2);
        }

        return result;
    }

    private void PrintCommandList()
    {
        Debug.LogError("[");
        //Debug.LogErrorFormat("Target:{0}", CarAutoRunManager.Instance.TargetLayer);
        _commandList.ForEach(command => command.Print());
        Debug.LogError("]");
    }


    public System.Action<CarMoveManager.Direction> OnRunTrendChanged;

    private CarMoveManager.Direction _carRunTrend = CarMoveManager.Direction.None;
    private CarMoveManager.Direction _CarRunTrend
    {
        get { return _carRunTrend; }
        set
        {
            if(_carRunTrend != value)
            {
                _carRunTrend = value;
                RefreshRunTrend();
            }
        }
    }

    private void RefreshRunTrend()
    {
        if (OnRunTrendChanged != null) OnRunTrendChanged(_carRunTrend);
    }


    private CarMoveManager.Direction ParseRunDirction()
    {
        if (_commandList.Count == 0) return CarMoveManager.Direction.None;

        CarRunCommand firstCarRunCommand = null;

        var firstCommand = _commandList[0] as CarDoorOpenCommand;
        if (firstCommand != null && firstCommand.Trend != null)
        {
            firstCarRunCommand = firstCommand.Trend;
        }
        
        if(firstCarRunCommand == null)
        {
            firstCarRunCommand = _commandList.Find(command => command.GetType().Equals(typeof(CarRunCommand))) as CarRunCommand;
        }

        if (firstCarRunCommand == null)
        {
            return CarMoveManager.Direction.None;
        }
        else
        {
            var targetHeight = ElevatorConst.ParseLayerHeight(firstCarRunCommand.TargetLayer);
            var currentHeight = CarMoveManager.Instance.GetCarHeight();
            if (Mathf.Equals(targetHeight, currentHeight))
            {
                return firstCarRunCommand.TargetDirection;
            }
            else if (targetHeight > currentHeight)
            {
                return CarMoveManager.Direction.Up;
            }
            else
            {
                return CarMoveManager.Direction.Down;
            }
        }
    }
}
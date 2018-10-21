using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorAutoRunManager : Singleton<CarDoorAutoRunManager>
{
    public enum DoorState
    {
        Closed,
        Opening,
        WholeOpened,
        Closing,
    }

    public DoorState CurrentDoorState
    {
        get
        {
            return _currentDoorState;
        }

        private set
        {
            _currentDoorState = value;
            switch(_currentDoorState)
            {
                case DoorState.WholeOpened:
                    _wholeOpenedDuration = 0.0f;
                    break;
            }
        }
    }

    private DoorState _currentDoorState = DoorState.Closed;
    private CarDoorMoveManager _carDoorMoveManger;
    private float _wholeOpenedDuration = 0.0f;
    public const float kWholeOpenedKeepTotalDuration = 2.0f;

    public CarDoorAutoRunManager()
    {
        _carDoorMoveManger = CarDoorMoveManager.Instance;
    }

    public void OpenDoor()
    {
        CurrentDoorState = DoorState.Opening;
    }

    public void CloseDoor()
    {
        CurrentDoorState = DoorState.Closing;
    }

    public void Update()
    {
        switch(_currentDoorState)
        {
            case DoorState.Closed:
                break;
            case DoorState.Opening:
                _carDoorMoveManger.MoveToDirection(CarDoorMoveManager.Direction.Open);
                if(_carDoorMoveManger.DoorState == CarDoorMoveManager.State.Opened)
                {
                    CurrentDoorState = DoorState.WholeOpened;
                }
                break;
            case DoorState.WholeOpened:
                if(_wholeOpenedDuration > kWholeOpenedKeepTotalDuration)
                {
                    CurrentDoorState = DoorState.Closing;
                }

                _wholeOpenedDuration += Time.deltaTime;
                break;
            case DoorState.Closing:
                _carDoorMoveManger.MoveToDirection(CarDoorMoveManager.Direction.Close);
                if (_carDoorMoveManger.DoorState == CarDoorMoveManager.State.Closed)
                {
                    CurrentDoorState = DoorState.Closed;
                }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorMoveManager : Singleton<CarDoorMoveManager>
{
    public enum State
    {
        Closed,
        SemiOpened,
        Opened,
    };

    public enum Direction
    {
        Stop,
        Open,
        Close,
    };

    private TractorDriverContainer _driverContainer = new TractorDriverContainer();

    public const string kCarDoorPartName = "CarDoorPart";
    public float CarDoorMoveVelocity = ElevatorConst.kCarDoorWidth * 0.5f;

    public CarDoorMoveManager()
    {
        _driverContainer.RegistryPart(TractorDriverContainer.kMainDriverName, kCarDoorPartName, new CarDoorMoveImpl());
        _driverContainer.MinProgress = 0;
        _driverContainer.MaxProgress = ElevatorConst.kCarDoorWidth;
    }

    /// <summary>
    /// void OnDoorMoved(float oldOpenWidth, float newOpenWidth)
    /// </summary>
    public System.Action<float, float> OnDoorMoved;

    public void SetDoorPos(float width)
    {
        var oldValue = GetDoorPos();
        _driverContainer.Progress = width;
        RefreshState();
        if(OnDoorMoved != null)
        {
            OnDoorMoved(oldValue, width);
        }
    }

    public float GetDoorPos()
    {
        return _driverContainer.Progress;
    }

    public void MoveToDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.Open:
                SetDoorPos(_driverContainer.Progress + CarDoorMoveVelocity * Time.deltaTime);
                break;
            case Direction.Close:
                SetDoorPos(_driverContainer.Progress - CarDoorMoveVelocity * Time.deltaTime);
                break;
        }
    }

    public State DoorState { get; private set; }

    private void RefreshState()
    {
        if (Mathf.Equals(_driverContainer.Progress, 0.0f))
        {
            DoorState = State.Closed;
        }
        else if (Mathf.Equals(_driverContainer.Progress, ElevatorConst.kCarDoorWidth))
        {
            DoorState = State.Opened;
        }
        else
        {
            DoorState = State.SemiOpened;
        }
    }
}

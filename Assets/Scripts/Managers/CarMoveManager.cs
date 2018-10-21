using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CarMoveManager : Singleton<CarMoveManager>
{
    public enum Direction
    {
        None,
        Up,
        Down
    };

    public const string kElevatorPartName = "ElevatorPart";
    private TractorDriverContainer _driverContainer = new TractorDriverContainer();

    public System.Action<int> OnCurrentLayerChanged;
    /// <summary>
    /// void OnCarMoved(float newCarHeight)
    /// </summary>
    public System.Action<float> OnCarMoved;

    public int CurrentLayer
    {
        get { return _currentLayer; }
        private set
        {
            if(_currentLayer != value)
            {
                _currentLayer = value;
                _upstairLayerHeight = ElevatorConst.ParseLayerHeight(_currentLayer + 1);
                _downstairLayerHeight = ElevatorConst.ParseLayerHeight(_currentLayer - 1);

                if (OnCurrentLayerChanged != null) OnCurrentLayerChanged(_currentLayer);
            }
        }
    } 
    public float CarMoveVelocity = 2.0f;

    public CarMoveManager()
    {
        _driverContainer.RegistryPart(TractorDriverContainer.kMainDriverName, kElevatorPartName, new CarMoveImpl());
        _driverContainer.Progress = ElevatorConst.kElevatorPositionYMin;
    }

    public override void Initialize()
    {
        base.Initialize();

        CurrentLayer = 1;
    }

    public void SetCarHeight(float height)
    {
        SetHeightInner(height);
    }

    public float GetCarHeight()
    {
        return _driverContainer.Progress;
    }

    public void SetCarLayer(int layer)
    {
        SetHeightInner(ElevatorConst.ParseLayerHeight(layer));
    }

    public void MoveToDirection(Direction direction)
    {
        float coefficient = 0;
        switch(direction)
        {
            case Direction.Down:
                coefficient = -1;
                break;
            case Direction.Up:
                coefficient = 1;
                break;
        }
        MoveOffsetInner(Time.deltaTime * CarMoveVelocity * coefficient);
    }

    public bool MoveToLayer(int layer)
    {
        var targetHeight = ElevatorConst.ParseLayerHeight(layer);
        var differHeight = targetHeight - _driverContainer.Progress;
        var coefficent = differHeight >= 0 ? 1 : -1;
        var absDifferHeight = Mathf.Abs(differHeight);
        var moveDistance = Mathf.Min(Time.deltaTime * CarMoveVelocity, absDifferHeight);
        MoveOffsetInner(coefficent * moveDistance);

        return Mathf.Equals(_driverContainer.Progress, targetHeight); 
    }
}

public partial class CarMoveManager : Singleton<CarMoveManager>
{
    private int _currentLayer = 0;
    private float _upstairLayerHeight;
    private float _downstairLayerHeight;

    private void RefreshCurrentLayer()
    {
        var currentHeight = GetCarHeight();
        if(currentHeight >= _upstairLayerHeight)
        {
            CurrentLayer = _currentLayer + 1;
        }
        else if(currentHeight <= _downstairLayerHeight)
        {
            CurrentLayer = _currentLayer - 1;
        }

        if (OnCarMoved != null)
        {
            OnCarMoved(currentHeight);
        }
    }

    private void SetHeightInner(float height)
    {
        var oldHeight = GetCarHeight();
        _driverContainer.Progress = height;
        RefreshCurrentLayer();
    }

    private void MoveOffsetInner(float offset)
    {
        _driverContainer.MoveOffset(offset);
        RefreshCurrentLayer();
    }
}
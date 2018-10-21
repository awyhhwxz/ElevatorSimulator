using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CarAutoRunManager : Singleton<CarAutoRunManager>
{

    public CarAutoRunManager()
    {
        _carMoveManger = CarMoveManager.Instance;
    }

    public int TargetLayer { get { return _targetLayer; }
        private set
        {
            if(_targetLayer != value)
            {
                _targetLayer = value;
                _targetLayerHeight = ElevatorConst.ParseLayerHeight(_targetLayer);
                RefreshDirection();
            }
        }
    }

    public System.Action<CarMoveManager.Direction> OnRunDirectionChanged;

    public CarMoveManager.Direction Direction
    {
        get
        {
            return _direction;
        }

        private set
        {
            if(_direction != value)
            {
                _direction = value;
                if(OnRunDirectionChanged != null)
                {
                    OnRunDirectionChanged(_direction);
                }
            }
        }
    }
    
    public void RunToLayer(int layer)
    {
        if(!Mathf.Equals(TargetLayer, layer))
        {
            TargetLayer = layer;
            IsStarted = true;
        }
    }

    public void Update()
    {
        if(IsStarted)
        {
            if(_carMoveManger.MoveToLayer(TargetLayer))
            {
                IsStarted = false;
            }

            RefreshDirection();
        }
    }
}

/// <summary>
/// private
/// </summary>
public partial class CarAutoRunManager : Singleton<CarAutoRunManager>
{

    private CarMoveManager _carMoveManger;
    
    private bool IsStarted = false;

    private float _targetLayerHeight = ElevatorConst.ParseLayerHeight(1);
    private int _targetLayer = 1;

    private CarMoveManager.Direction _direction = CarMoveManager.Direction.None;

    private void RefreshDirection()
    {
        if (Mathf.Equals(_targetLayerHeight, _carMoveManger.GetCarHeight()))
        {
            Direction = CarMoveManager.Direction.None;
        }
        else if (_targetLayerHeight > _carMoveManger.GetCarHeight())
        {
            Direction = CarMoveManager.Direction.Up;
        }
        else
        {
            Direction = CarMoveManager.Direction.Down;
        }
    }
}
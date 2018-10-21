using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallDoorMoveHandler {

    private TractorDriverContainer _driverContainer = new TractorDriverContainer();

    public const string kHallDoorPartName = "HallDoorPart";

    private float _linkedCarMinHeight = 0;
    private float _linkedCarMaxHeight = 0;

    public HallDoorMoveHandler(int layer)
    {
        _driverContainer.RegistryPart(TractorDriverContainer.kMainDriverName, kHallDoorPartName, new HallDoorMoveImpl(layer));
        _driverContainer.MinProgress = 0;
        _driverContainer.MaxProgress = ElevatorConst.kCarDoorWidth;

        var layerHeight = ElevatorConst.ParseLayerHeight(layer);
        _linkedCarMinHeight = layerHeight - ElevatorConst.kCarLevelOffOffset;
        _linkedCarMaxHeight = layerHeight + ElevatorConst.kCarLevelOffOffset;
    }

    public void SetDoorPos(float width)
    {
        _driverContainer.Progress = width;
    }

    public float GetDoorPos()
    {
        return _driverContainer.Progress;
    }

    public void OnCarMoved(float carHeight)
    {
        IsLinkedCarDoor = carHeight >= _linkedCarMinHeight && carHeight <= _linkedCarMaxHeight;
    }

    private bool _isLinkedCarDoor = false;
    public bool IsLinkedCarDoor { get { return _isLinkedCarDoor; }
        private set
        {
            if(_isLinkedCarDoor != value)
            {
                _isLinkedCarDoor = value;
                if(_isLinkedCarDoor)
                {
                    CarDoorMoveManager.Instance.OnDoorMoved += OnCarDoorMoved;
                }
                else
                {
                    CarDoorMoveManager.Instance.OnDoorMoved -= OnCarDoorMoved;
                }
            }
        }
    }

    private void OnCarDoorMoved(float oldOpenWidth, float newOpenWidth)
    {
        if(IsLinkedCarDoor)
        {
            var carDoorDelta = newOpenWidth - oldOpenWidth;
            var hallDoorDelta = newOpenWidth - GetDoorPos();
            if(carDoorDelta * hallDoorDelta > 0)
            {
                SetDoorPos(newOpenWidth);
            }
        }
    }
}

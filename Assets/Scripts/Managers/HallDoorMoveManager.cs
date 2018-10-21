using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallDoorMoveManager : Singleton<HallDoorMoveManager> {

    private Dictionary<int, HallDoorMoveHandler> _doorMoveHandlerDic = new Dictionary<int, HallDoorMoveHandler>();

	public HallDoorMoveManager()
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        for (int layerI = 1; layerI <= ElevatorConst.kLayerCount; ++layerI)
        {
            _doorMoveHandlerDic.Add(layerI, new HallDoorMoveHandler(layerI));
        }

        InitializeCarMoveCallBack();
    }

    private void InitializeCarMoveCallBack()
    {
        CarMoveManager.Instance.OnCarMoved += OnCarMoved;
        OnCarMoved(CarMoveManager.Instance.GetCarHeight());
    }

    private void OnCarMoved(float newCarHeight)
    {
        foreach(var pair in _doorMoveHandlerDic)
        {
            var handler = pair.Value;
            handler.OnCarMoved(newCarHeight);
        }
    }
}

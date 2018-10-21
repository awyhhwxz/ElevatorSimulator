using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMoveImpl : ITractorPart
{
    private GameObject _carRootObj;
    protected GameObject _CarRootObj
    {
        get
        {
            if(_carRootObj == null)
            {
                _carRootObj = GameObject.Find("Elevator");
            }

            return _carRootObj;
        }
    }

    public string Name { get; set; }

    public void SetProgress(float progress, float scale)
    {
        if(_CarRootObj)
        {
            var position = ElevatorConst.kElevatorOriginPosition;
            position.y = progress;
            _CarRootObj.transform.position = position;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDoorMoveImpl : ITractorPart
{
    public string Name { get; set; }

    private GameObject _carDoorRoot;
    private GameObject _carLeftDoorObj;
    private GameObject _carRightDoorObj;

    protected GameObject _CarDoorRoot
    {
        get
        {
            if(_carDoorRoot == null)
            {
                _carDoorRoot = GameObject.Find("CarDoorRoot");
                if(_carDoorRoot != null)
                {
                    _carLeftDoorObj = _carDoorRoot.transform.Find("CarDoorLeft").gameObject;
                    _carRightDoorObj = _carDoorRoot.transform.Find("CarDoorRight").gameObject;
                }
            }

            return _carDoorRoot;
        }
    }

    public void SetProgress(float progress, float scale)
    {
        if(_CarDoorRoot != null)
        {
            _carLeftDoorObj.transform.localPosition = new Vector3(-progress, 0, 0);
            _carRightDoorObj.transform.localPosition = new Vector3(progress, 0, 0);
        }
    }
}

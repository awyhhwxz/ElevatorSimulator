using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallDoorMoveImpl : ITractorPart
{
    public string Name { get; set; }

    private int _layer = -1;
    public HallDoorMoveImpl(int layer)
    {
        _layer = layer;
    }
    
    private GameObject _hallDoorRoot;
    private GameObject _hallLeftDoorObj;
    private GameObject _hallRightDoorObj;

    protected GameObject _HallDoorRoot
    {
        get
        {
            if (_hallDoorRoot == null)
            {
                _hallDoorRoot = GameObject.Find(string.Format("HallDoorRoot_{0:D3}", _layer));
                if (_hallDoorRoot != null)
                {
                    _hallLeftDoorObj = _hallDoorRoot.transform.Find(string.Format("HallDoorLeft_{0:D3}", _layer)).gameObject;
                    _hallRightDoorObj = _hallDoorRoot.transform.Find(string.Format("HallDoorRight_{0:D3}", _layer)).gameObject;
                }
            }

            return _hallDoorRoot;
        }
    }

    public void SetProgress(float progress, float scale)
    {
        if (_HallDoorRoot != null)
        {
            _hallLeftDoorObj.transform.localPosition = new Vector3(-progress, 0, 0);
            _hallRightDoorObj.transform.localPosition = new Vector3(progress, 0, 0);
        }
    }
}

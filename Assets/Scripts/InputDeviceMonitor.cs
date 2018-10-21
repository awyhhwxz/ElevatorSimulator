using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDeviceMonitor : MonoBehaviour {

    void Awake()
    {
        RoleManager.Instance.Initialize();
        PartManager.Instance.Initialize();
        CarMoveManager.Instance.Initialize();
        HallDoorMoveManager.Instance.Initialize();
        CommandManager.Instance.Initialize();
        CarCommandReciever.Instance.Initialize();
    }

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ScreenRayCastManager.Instance.Cast(ray);
        }

        CarCommandReciever.Instance.Update();
    }
}

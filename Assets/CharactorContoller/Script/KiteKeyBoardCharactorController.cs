using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteKeyBoardCharactorController : MonoBehaviour {

    private KiteCharactorContollerHandler _handler = new KiteCharactorContollerHandler();
    
    void Awake()
    {
        _handler.CharactorInfo.CharactorObj = this.gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _handler.GoToLeft();
        }
        if (Input.GetKey(KeyCode.S))
        {
            _handler.GoToBack();
        }
        if (Input.GetKey(KeyCode.D))
        {
            _handler.GoToRight();
        }
        if (Input.GetKey(KeyCode.W))
        {
            _handler.GoToForward();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _handler.Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _handler.ChangeCrouchState();
        }

        if (Input.GetMouseButton(1))
        {
            _handler.SetHorizontalOffset(Input.GetAxis("Mouse X"));
        }

        if(Input.GetMouseButton(1))
        {
            _handler.SetVerticalOffset(Input.GetAxis("Mouse Y"));
        }

        _handler.Update();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PhysicsEventManager.Instance.InvokeEvent(PhysicsEventManager.HandlerType.Role, PhysicsEventType.TriggerEnter
            , new PhysiscsEventArgs() { TriggerCollider = other, RigidbodyObject = this.gameObject });
    }

    private void OnTriggerExit(Collider other)
    {
        PhysicsEventManager.Instance.InvokeEvent(PhysicsEventManager.HandlerType.Role, PhysicsEventType.TriggerExit
            , new PhysiscsEventArgs() { TriggerCollider = other, RigidbodyObject = this.gameObject });
    }
}

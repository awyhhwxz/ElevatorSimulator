using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class KiteCharactorContollerHandler
{

    private KiteCharactorContollerHandlerImpl _charactorHandlerImpl = new KiteCharactorContollerHandlerImpl();
    private KiteCharactorCameraHandler _cameraHandler = new KiteCharactorCameraHandler();
    private KiteCharactorCrouchHandler _crouchHandler = new KiteCharactorCrouchHandler();

    public KiteCharactorInfo CharactorInfo = new KiteCharactorInfo();

    public bool IsEnable
    {

        get { return _isEnable; }
        set
        {
            if(_isEnable != value)
            {
                _isEnable = value;
                RefreshRigidbody();
            }
        }
    }

    public KiteCharactorContollerHandler()
    {
        _charactorHandlerImpl.CharactorInfo = CharactorInfo;
        _cameraHandler.CharactorInfo = CharactorInfo;
        _crouchHandler.CharactorInfo = CharactorInfo;
    }

    /// <summary>
    /// Must be invoke at the end of a frame
    /// </summary>
    public void Update()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.Update();
    }

    public void GoToForward()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.GoToForward();
    }

    public void GoToBack()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.GoToBack();
    }

    public void GoToLeft()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.GoToLeft();
    }

    public void GoToRight()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.GoToRight();
    }

    public void Jump()
    {
        if (IsCharactorStateChangable) _charactorHandlerImpl.Jump();
    }

    public void ChangeCrouchState()
    {
        if (IsCharactorStateChangable) _crouchHandler.ChangeCrouchState();
    }

        public void SetHorizontalOffset(float offset)
    {
        if (IsCharactorStateChangable) _cameraHandler.SetHorizontalOffset(offset);
    }

    public void SetVerticalOffset(float offset)
    {
        if (IsCharactorStateChangable) _cameraHandler.SetVerticalOffset(offset);
    }
}

public partial class KiteCharactorContollerHandler
{

    protected bool IsCharactorStateChangable { get { return IsEnable && CharactorInfo.CharactorObj; } }

    private bool _isEnable = true;
    
    protected void RefreshRigidbody()
    {
        var charactorRigidBody = _charactorHandlerImpl.CharactorRigidBody;
        if (_isEnable)
        {
            charactorRigidBody.isKinematic = false;
            charactorRigidBody.useGravity = true;
        }
        else
        {
            charactorRigidBody.isKinematic = true;
            charactorRigidBody.useGravity = false;
        }
    }
}
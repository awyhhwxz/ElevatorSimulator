using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class KiteCharactorInfo {

    public float WalkSpeed = 4.0f;
    public float StartAcceleratedSpeed = 8.0f;
    public GameObject CharactorObj { get; set; }

    public float CameraHorizontalSpeed = 10.0f;
    public float CameraVerticalSpeed = 10.0f;

    public float CameraMaxVerticalAngle = 60.0f;
    public float CameraMinVerticalAngle = -60.0f;

    public float JumpForce = 2.0f;

    public float StepHeight = 0.3f;

    public float RoleStandHeight = 1.7f;
}

public partial class KiteCharactorInfo
{
    private CapsuleCollider _capsule;

    public CapsuleCollider Capsule
    {
        get
        {
            if (_capsule == null)
            {
                var charactorObj = CharactorObj;
                if (charactorObj)
                {
                    _capsule = charactorObj.GetComponent<CapsuleCollider>();
                }
            }

            return _capsule;
        }
    }
    
    private GameObject _cameraObj = null;
    public GameObject CameraObj
    {
        get
        {
            if (_cameraObj == null)
            {
                var charactorObj = CharactorObj;
                if (charactorObj)
                {
                    var eyeBase = charactorObj.transform.Find("EyeBase");
                    if (eyeBase)
                    {
                        _cameraObj = eyeBase.gameObject;
                    }
                }
            }

            return _cameraObj;
        }
    }
}
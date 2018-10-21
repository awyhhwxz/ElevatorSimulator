using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManager : Singleton<RoleManager>
{

    private GameObject _roleObj;
    public GameObject RoleObj
    {
        get
        {
            return _roleObj;
        }

        set
        {
            if (_roleObj != value)
            {
                _roleObj = value;
                if (OnRoleObjChanged != null)
                {
                    OnRoleObjChanged(_roleObj);
                }
            }
        }
    }

    public System.Action<GameObject> OnRoleObjChanged;

    public const float kRoleRadius = 0.3f;

    public override void Initialize()
    {
        RoleObj = GameObject.Find("Charactor");
    }
}

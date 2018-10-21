using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteCharactorCrouchHandler {

    public KiteCharactorInfo CharactorInfo { get; set; }

    public void ChangeCrouchState()
    {
        if(CharactorInfo.Capsule.height > CharactorInfo.RoleStandHeight * 0.75f)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }
    }

    public void StandUp()
    {
        ChangeRoleHeight(CharactorInfo.RoleStandHeight);
    }

    public void Crouch()
    {
        ChangeRoleHeight(CharactorInfo.RoleStandHeight * 0.5f);
    }

    protected void ChangeRoleHeight(float height)
    {
        var capsule = CharactorInfo.Capsule;
        var preHeight = capsule.height;
        if(!Mathf.Equals(preHeight, height))
        {
            var heightDifferent = height - preHeight;
            capsule.height = height;
            var charactorObj = CharactorInfo.CharactorObj;
            charactorObj.transform.Translate(Vector3.up * heightDifferent * 0.5f);

            CharactorInfo.CameraObj.transform.localPosition = new Vector3(0, height * 0.5f, 0);
        }
    }
}

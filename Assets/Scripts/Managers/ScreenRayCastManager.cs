using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRayCastManager : Singleton<ScreenRayCastManager>
{

    private Dictionary<int, IObjectCaster> _casterDic = new Dictionary<int, IObjectCaster>()
    {
        { LayerTagConst.kInteractiveObjectLayer, new InteractiveObjectCaster() }
    };

    public void Cast(Ray ray)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            var layer = hitInfo.collider.gameObject.layer;
            IObjectCaster caster = null;
            if(_casterDic.TryGetValue(layer, out caster))
            {
                caster.Cast(hitInfo);
            }
        }
    }
}

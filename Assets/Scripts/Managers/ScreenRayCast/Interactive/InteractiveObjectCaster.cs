using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjectCaster : IObjectCaster
{
    private Dictionary<string, IObjectCaster> _casterItemDictionary = new Dictionary<string, IObjectCaster>()
    {
        { LayerTagConst.kCopButtonTagName, new CopButtonScreenRayCastItem() },
        { LayerTagConst.kLopButtonTagName, new LopButtonScreenRayCastItem() },
    };

    public void Cast(RaycastHit hitInfo)
    {
        var tag = hitInfo.collider.gameObject.tag;
        IObjectCaster caster = null;
        if (_casterItemDictionary.TryGetValue(tag, out caster))
        {
            caster.Cast(hitInfo);
        }
    }
}

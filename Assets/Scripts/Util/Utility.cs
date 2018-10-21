using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {
    
	public static void ResetTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
        t.localScale = Vector3.one;
    }

    public static void TraverseGameObject(Transform rootObj, System.Action<Transform> callback)
    {
        callback(rootObj);
        foreach(Transform child in rootObj)
        {
            TraverseGameObject(child, callback);
        }
    }

    public static T GetComponent<T>(GameObject obj) where T : MonoBehaviour
    {
        var comp = obj.GetComponent<T>();
        if(comp == null)
        {
            comp = obj.AddComponent<T>();
        }
        return comp;
    }

    public static void GenerateBoxCollider(Transform sourceObj, Transform targetObj = null, float margin = 0)
    {
        var collider = sourceObj.GetComponent<Collider>();
        if(collider == null)
        {            
            Renderer[] renders = sourceObj.GetComponentsInChildren<Renderer>();

            Vector3 minPoint = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 maxPoint = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (Renderer child in renders)
            {
                var meshBounds = child.GetComponent<MeshFilter>().sharedMesh.bounds;
                var min = sourceObj.InverseTransformPoint(child.transform.TransformPoint(meshBounds.min));
                var max = sourceObj.InverseTransformPoint(child.transform.TransformPoint(meshBounds.max));
                minPoint = Vector3.Min(minPoint, min);
                maxPoint = Vector3.Max(maxPoint, min);
                minPoint = Vector3.Min(minPoint, max);
                maxPoint = Vector3.Max(maxPoint, max);
            }

            //It's a semi-finished function, target object transform is untreated.But it's enough now.I'll complete it later.
            var finalObj = targetObj == null ? sourceObj : targetObj;
            BoxCollider boxCollider = finalObj.gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.size = maxPoint - minPoint - new Vector3(margin, margin, margin);
            boxCollider.center = (maxPoint + minPoint) * 0.5f;
        }

    }
}

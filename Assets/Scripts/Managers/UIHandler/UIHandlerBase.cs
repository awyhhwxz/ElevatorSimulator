using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandlerBase : IUIHandler
{
    public Transform Root { get; set; }

    public void SetVisible(bool isVisible)
    {
        if(Root)
        {
            Root.gameObject.SetActive(isVisible);
        }
    }
}

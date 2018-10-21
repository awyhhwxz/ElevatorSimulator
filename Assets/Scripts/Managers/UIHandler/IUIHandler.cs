using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIHandler {

    Transform Root { get; }

    void SetVisible(bool isVisible);
}

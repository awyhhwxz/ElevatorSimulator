using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTagConst {

    public static readonly int kDefaultLayer;
    public static readonly int kInteractiveObjectLayer;
    public static readonly int kRoleMoveAreaLayer;

    //InteractiveObject
    public const string kCopButtonTagName = "CopButton";
    public const string kLopButtonTagName = "LopButton";

    //RoleMoveArea
    public const string kTakeElevatorAreaTagName = "TakeElevatorArea";

    static LayerTagConst()
    {
        kDefaultLayer = LayerMask.NameToLayer("Default");
        kInteractiveObjectLayer = LayerMask.NameToLayer("InteractiveObject");
        kRoleMoveAreaLayer = LayerMask.NameToLayer("RoleMoveArea");
    }
}

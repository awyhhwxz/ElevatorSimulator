using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorConst {

    public const int kLayerCount = 4;
    public const float kLayerHeight = 4.5f;
    public const float kCarLevelOffOffset = 0.25f;
    public const string kElevatorRootObjName = "Elevator";
    public static readonly Vector3 kElevatorOriginPosition = new Vector3(0, -1, -7.500318f);
    public const float kElevatorPositionYMin = -1f;
    public const float kElevatorPositionYMax = kElevatorPositionYMin + kLayerHeight * (kLayerCount - 1);
    public const float kElevatorPositionTotalHeight = kElevatorPositionYMax - kElevatorPositionYMin; 

    public const float kCarDoorWidth = 0.8f;

    public const string kCopButtonDisplayNameRegex = "CopButtonDisplay([0-9]+)";
    public const string kCopOpenDoorButtonName = "CopButtonDisplayOpenDoor";
    public const string kCopCloseDoorButtonName = "CopButtonDisplayCloseDoor";

    public const string kLopButtonDisplayNameRegex = "LopButton(Up|Down)Display_([0-9]+)";
    public const string kUp = "Up";
    public const string kDown = "Down";

    public static float ParseLayerHeight(int layer)
    {
        return ElevatorConst.kElevatorPositionYMin + (layer - 1) * ElevatorConst.kLayerHeight;
    }

    public static float ParseLayerHeight(float layer)
    {
        var ceilHeight = ParseLayerHeight(Mathf.Ceil(layer));
        var floorHeight = ParseLayerHeight(Mathf.Floor(layer));
        return Mathf.Lerp(floorHeight, ceilHeight, layer - floorHeight);
    }

    public static float ParseHeightComplementarySetLength(float height)
    {
        return kElevatorPositionTotalHeight - ParseHeightLength(height);
    }

    public static float ParseHeightLength(float height)
    {
        return height - kElevatorPositionYMin;
    }
}

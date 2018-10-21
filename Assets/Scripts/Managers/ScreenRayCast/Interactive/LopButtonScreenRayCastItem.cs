using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LopButtonScreenRayCastItem : IObjectCaster
{
    public void Cast(RaycastHit hitInfo)
    {
        var targetButtonObj = hitInfo.collider.gameObject;
        var stateHandler = Utility.GetComponent<ButtonStateHandler>(targetButtonObj);

        var regex = new Regex(ElevatorConst.kLopButtonDisplayNameRegex);
        var targetButtonObjName = targetButtonObj.name;
        var match = regex.Match(targetButtonObjName);
        if (match.Success)
        {
            var directionTag = match.Groups[1].Value;
            var direction = CarMoveManager.Direction.None;
            if (directionTag == ElevatorConst.kUp) direction = CarMoveManager.Direction.Up;
            else if (directionTag == ElevatorConst.kDown) direction = CarMoveManager.Direction.Down;
            var layer = int.Parse(match.Groups[2].Value);

            CommandManager.Instance.SendCommand(CommandSenderType.CarCommand
                , BaseCommandType.CarRunCommand
                , (int)CarRunCommandFactory.CommandType.LopButtonCast
                , new OpNumButtonCastArgs() { TargetLayer = layer, TargetDirection = direction, StateHandler = stateHandler });
            //CarCommandReciever.Instance.SendCommand(new CarRunCommand()
            //{
            //    TargetLayer = layer,
            //    TargetDirection = direction,
            //    OnEventAdd = () =>
            //    {
            //        stateHandler.SetButtonState(true);
            //    },
            //    OnEventEnd = () =>
            //    {
            //        stateHandler.SetButtonState(false);
            //        CarCommandReciever.Instance.SendCommand(new CarDoorOpenCommand() { IsOpen = true, ForceAdd = true });
            //    }
            //});
        }
    }
}

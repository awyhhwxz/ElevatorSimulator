using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CopButtonScreenRayCastItem : IObjectCaster
{
    public void Cast(RaycastHit hitInfo)
    {
        var targetButtonObj = hitInfo.collider.gameObject;
        var stateHandler = Utility.GetComponent<ButtonStateHandler>(targetButtonObj);

        var regex = new Regex(ElevatorConst.kCopButtonDisplayNameRegex);
        var targetButtonObjName = targetButtonObj.name;
        var match = regex.Match(targetButtonObjName);
        if(match.Success)
        {
            var layer = int.Parse(match.Groups[1].Value);
            CommandManager.Instance.SendCommand(CommandSenderType.CarCommand
                , BaseCommandType.CarRunCommand
                , (int)CarRunCommandFactory.CommandType.CopNumButtonCast
                , new OpNumButtonCastArgs() { TargetLayer = layer, StateHandler = stateHandler });
            //CarCommandReciever.Instance.SendCommand(new CarRunCommand()
            //{
            //    TargetLayer = layer,
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
        else if(targetButtonObjName.Equals(ElevatorConst.kCopOpenDoorButtonName))
        {
            CommandManager.Instance.SendCommand(CommandSenderType.CarCommand
                , BaseCommandType.CarDoorOpenCommand
                , (int)CarDoorOpenCommandFactory.CommandType.CopDoorButtonCast
                , new CopDoorButtonCastArgs() { IsOpen = true, StateHandler = stateHandler });
            //CarCommandReciever.Instance.SendCommand(new CarDoorOpenCommand() { IsOpen = true,
            //    OnEventAdd = () =>
            //    {
            //        stateHandler.ActiveInAFlash();
            //    }
            //});
        }
        else if(targetButtonObjName.Equals(ElevatorConst.kCopCloseDoorButtonName))
        {
            CommandManager.Instance.SendCommand(CommandSenderType.CarCommand
                , BaseCommandType.CarDoorOpenCommand
                , (int)CarDoorOpenCommandFactory.CommandType.CopDoorButtonCast
                , new CopDoorButtonCastArgs() { IsOpen = false, StateHandler = stateHandler });
            //CarCommandReciever.Instance.SendCommand(new CarDoorOpenCommand() { IsOpen = false,
            //    OnEventAdd = () =>
            //    {
            //        stateHandler.ActiveInAFlash();
            //    }
            //});
        }
    }
}

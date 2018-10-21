using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCommandSender : ICommandSender
{
    public void Send(Command command)
    {
        CarCommandReciever.Instance.SendCommand(command);
    }
}

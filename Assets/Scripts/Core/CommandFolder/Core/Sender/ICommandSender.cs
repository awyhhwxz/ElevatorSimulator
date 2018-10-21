using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandSender {

    void Send(Command command);
}

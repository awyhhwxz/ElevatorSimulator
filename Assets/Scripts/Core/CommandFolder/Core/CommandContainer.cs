using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandContainer {

    public int CommandFactoryType = -1;

    public CommandContainer(EventArgs args) { }
    public Command TargetCommand { get;set;}

    public abstract void AssignArgs(EventArgs args);
}

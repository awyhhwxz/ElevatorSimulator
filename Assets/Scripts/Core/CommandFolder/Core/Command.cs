using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command {

    public delegate void RefListCommandDelegate(ref Command currentCommand, ref List<Command> commandList);

    public Action OnEventAdd;
    public Action OnEventStart;
    public Action OnEventEnd;

    public Func<bool> IsCompleted;
    public Action HandleEvent;

    public RefListCommandDelegate AddCommand;

    public abstract void CopyFrom(Command command);

    public virtual int SortCompareFunction(Command command1, Command command2)
    {
        return 0;
    }

    public abstract void Print();

    public abstract int Priority { get; }
}

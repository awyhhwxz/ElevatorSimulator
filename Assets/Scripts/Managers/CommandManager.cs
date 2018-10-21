using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : Singleton<CommandManager> {

    public class CommandArgsStruct
    {
        public int SenderType;
        public int FactoryType;
        public int CommandType;
        public EventArgs Args;
    }

    private CommandSenderSelector _senderSelector = new CommandSenderSelector();
    private CommandFactorySelector _factorySelector = new CommandFactorySelector();
    
    public CommandManager()
    {
    }

    private EventArgs CreateArgs(int factoryType, int commandType)
    {
        EventArgs args = null;
        var factory = _factorySelector.Select(factoryType);
        if (factory != null)
        {
            args = factory.CreateArgs(commandType);
        }

        return args;
    }

    public void SendCommand(CommandSenderType senderType, BaseCommandType factoryType, int commandType, EventArgs args)
    {
        SendCommand((int)senderType, (int)factoryType, commandType, args);
    }

    public void SendCommand(int senderType, int factoryType, int commandType, EventArgs args)
    {
        CommandArgsStruct argsStruct = new CommandArgsStruct()
        {
            SenderType = senderType,
            FactoryType = factoryType,
            CommandType = commandType,
            Args = args,
        };
        
        SendCommandInner(argsStruct);
    }

    public void SendCommandInner(CommandArgsStruct argsStruct)
    {
        var factory = _factorySelector.Select(argsStruct.FactoryType);
        if (factory != null)
        {
            var command = factory.Create(argsStruct.CommandType, argsStruct.Args);
            if (command != null)
            {
                var sender = _senderSelector.Select(argsStruct.SenderType);
                if (sender != null)
                {
                    sender.Send(command.TargetCommand);
                }
            }
        }

    }
}

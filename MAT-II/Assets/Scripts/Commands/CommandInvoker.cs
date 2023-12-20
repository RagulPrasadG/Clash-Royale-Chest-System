using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    public Stack<ICommand> commands;
    public CommandInvoker()
    {
        this.commands = new Stack<ICommand>();
    }

    public void ProcessCommand(ICommand command)
    {
        commands.Push(command);
        command.Execute();
    }

    public void Undo()
    {
        ICommand command;
        commands.TryPop(out command);
        if (command != null)
            command.Undo();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    public Stack<ICommand> commands;
    public CommandInvoker(Stack<ICommand> commands)
    {
        this.commands = commands;
    }

    public void ProcessCommand(ICommand command)
    {
        commands.Push(command);
        command.Execute();
    }

    public void Undo()
    {
        commands.Pop().Execute();
    }

}

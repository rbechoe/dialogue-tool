using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSystem
{
    public List<Command> doActions = new List<Command>();

    private int actionPosition;

    public void AddAction(Command command)
    {
        actionPosition++;

        if (actionPosition < doActions.Count)
        {
            // remove all following actions as we are most likely inbetween the stack
        }

        command.Execute();

        doActions.Add(command);
    }
}

public class Command
{
    public Command() { }
    public virtual void Execute() { }
}

public class CreateNode : Command
{
    public override void Execute()
    {
        EventSystem.InvokeEvent(EventTypes.CreateNode);
    }
}

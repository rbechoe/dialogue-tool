using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSystem
{
    public Stack<System.Action> doActions = new Stack<System.Action>();

    private int actionPosition;

    public void AddAction(System.Action doAction)
    {
        actionPosition++;

        if (actionPosition < doActions.Count)
        {
            // remove all following actions as we are most likely inbetween the stack
        }

        doActions.Push(doAction);
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
        //CreateNode();
    }
}

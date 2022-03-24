using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : BaseCommand
{
    ToolManager manager;

    public override void Execute(ToolManager manager)
    {
        this.manager = manager;
        EventSystem.InvokeEvent(EventTypes.CreateNode);
    }

    public override void Redo()
    {
        // add gameobject at position
    }

    public override void Undo()
    {
        // remove gameobject
    }
}

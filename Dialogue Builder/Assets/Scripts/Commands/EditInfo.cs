using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInfo : BaseCommand
{
    public string oldName;
    public string newName;
    public string oldText;
    public string newText;
    public NodeObject myNode;

    public override void Execute()
    {
        myNode.SetText(newText);
        myNode.SetName(newName);
    }

    public override void Redo()
    {
        myNode.SetText(newText);
        myNode.SetName(newName);
    }

    public override void Undo()
    {
        myNode.SetText(oldText);
        myNode.SetName(oldName);
    }
}

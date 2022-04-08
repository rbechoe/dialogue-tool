using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNode : BaseCommand
{
    public Vector3 oldPosition;
    public Vector3 newPosition;
    public GameObject myNode;

    public override void Execute()
    {
        myNode.transform.position = newPosition;
    }

    public override void Redo()
    {
        myNode.transform.position = newPosition;
    }

    public override void Undo()
    {
        myNode.transform.position = oldPosition;
    }
}

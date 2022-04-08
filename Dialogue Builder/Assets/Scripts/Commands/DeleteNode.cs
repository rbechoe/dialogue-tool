using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteNode : BaseCommand
{
    public Vector3 startPosition;
    public GameObject myNode;

    public override void Execute()
    {
        startPosition = ToolManager.Instance.GetActiveNode().transform.position;
        myNode = ToolManager.Instance.GetActiveNode();
        myNode.gameObject.SetActive(false);
        ToolManager.Instance.RemovedNode(myNode);
        ToolManager.Instance.SetActiveNode(null);
    }

    public override void Redo()
    {
        myNode.gameObject.SetActive(false);
        ToolManager.Instance.RemovedNode(myNode);
        ToolManager.Instance.SetActiveNode(null);
    }

    public override void Undo()
    {
        myNode.gameObject.SetActive(true);
        ToolManager.Instance.CreatedNode(myNode);
        ToolManager.Instance.SetActiveNode(myNode);
    }
}

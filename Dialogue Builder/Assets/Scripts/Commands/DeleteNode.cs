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
        ToolManager.Instance.RemovedNode(ToolManager.Instance.GetActiveNode());
        Destroy(ToolManager.Instance.GetActiveNode());
        ToolManager.Instance.SetActiveNode(null);
    }

    public override void Redo()
    {
        ToolManager.Instance.RemovedNode(ToolManager.Instance.GetActiveNode());
        Destroy(ToolManager.Instance.GetActiveNode());
        ToolManager.Instance.SetActiveNode(null);
    }

    public override void Undo()
    {
        GameObject newNode = Instantiate(CommandManager.Instance.nodePrefab, startPosition, Quaternion.identity);
        myNode = newNode;
        ToolManager.Instance.CreatedNode(myNode);
        ToolManager.Instance.SetActiveNode(myNode);
    }
}

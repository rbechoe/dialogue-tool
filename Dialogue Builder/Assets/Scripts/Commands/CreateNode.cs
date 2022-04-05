using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : BaseCommand
{
    public Vector3 startPosition;
    public GameObject myNode;

    public override void Execute()
    {
        startPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        GameObject newNode = Instantiate(CommandManager.Instance.nodePrefab, startPosition, Quaternion.identity);
        myNode = newNode;
    }

    public override void Redo()
    {
        GameObject newNode = Instantiate(CommandManager.Instance.nodePrefab, startPosition, Quaternion.identity);
        myNode = newNode;
    }

    public override void Undo()
    {
        Destroy(myNode);
        myNode = null;
    }
}

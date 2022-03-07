using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnector : MonoBehaviour
{
    private Material myMat;
    private Color defaultCol;
    public NodeObject parent;

    private bool entered;
    
    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        defaultCol = myMat.color;
    }
    
    void Update()
    {
        if (entered && Input.GetMouseButtonDown(0))
        {
            if (gameObject.CompareTag("input"))
            {
                parent.manager.SetInputNode(parent.gameObject);
            }

            if (gameObject.CompareTag("output"))
            {
                parent.manager.SetOutputNode(parent.gameObject);
            }
        }
    }

    private void OnMouseEnter()
    {
        myMat.color = Color.green;
        entered = true;
    }

    private void OnMouseExit()
    {
        myMat.color = defaultCol;
        entered = false;
    }
}

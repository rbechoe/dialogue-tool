using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnector : MonoBehaviour
{
    private Material myMat;
    private Color defaultCol;
    private Transform mousePos;
    public NodeObject parent;
    public LineRenderer lineObject;

    private bool hovered;
    private bool isDragged;
    
    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        mousePos = GameObject.FindGameObjectWithTag("Player").transform;
        defaultCol = myMat.color;
        lineObject = parent.lineObject;
    }
    
    void Update()
    {
        if (hovered)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // line will go from here to other node
                if (gameObject.CompareTag("output"))
                {
                    parent.manager.SetOutputNode(parent.gameObject);
                    isDragged = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // receive line from other node to this position
                if (gameObject.CompareTag("input"))
                {
                    parent.manager.SetInputNode(parent.gameObject);
                }
            }
        }

        if (isDragged && Input.GetMouseButtonUp(0))
        {
            isDragged = false;

            // reset line in case user didnt connect
            lineObject.SetPosition(1, Vector3.zero);
        }

        // update line
        if (isDragged)
        {
            lineObject.transform.position = transform.position + Vector3.up * .25f;
            Vector3 position = (mousePos.position - lineObject.transform.position) + Vector3.up * .25f;

            lineObject.SetPosition(1, position);
        }
    }

    private void OnMouseEnter()
    {
        hovered = true;

        if (gameObject.CompareTag("input")) return;
        myMat.color = Color.green;
    }

    private void OnMouseExit()
    {
        hovered = false;

        if (gameObject.CompareTag("input")) return;
        myMat.color = defaultCol;
    }
}

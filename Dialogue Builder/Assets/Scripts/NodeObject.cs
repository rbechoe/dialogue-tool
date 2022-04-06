using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NodeObject : MonoBehaviour
{
    [Header("Settings")]
    public Color selectCol;
    public Color defaultCol;
    public Color highlightCol;
    public GameObject inputSphere;
    public GameObject outputSphere;
    public NodeObject inputObject;
    public NodeObject outputObject;
    public LineRenderer lineObject;

    private Material myMat;
    public ToolManager manager;

    public bool isSelected;
    private bool isHovered;
    private bool isDragged;
    private Transform mousePos;

    public TextMeshPro nameObj;

    // Trackables
    private Node nodeData;
    private string myName = "Node X";
    private string myText = null;
    private string myId = null;
    private string myInput = null;
    private string myOutput = null;
    private Languages language = Languages.English;

    private void Awake()
    {
        nodeData = new Node();
    }

    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        manager = ToolManager.Instance;
        mousePos = GameObject.FindGameObjectWithTag("Player").transform;
        myMat.color = defaultCol;
    }

    void Update()
    {
        if (isDragged && Input.GetMouseButtonUp(0))
        {
            isDragged = false;
        }

        if (isDragged)
        {
            Vector3 newPos = new Vector3(mousePos.position.x, transform.position.y, mousePos.position.z);
            transform.position = newPos;
            UpdateNode();
        }

        if (isHovered && !isSelected)
        {
            myMat.color = highlightCol;

            // select
            if (Input.GetMouseButtonDown(0))
            {
                manager.SetActiveNode(gameObject);
            }

            // allow object to be dragged
            if (Input.GetMouseButton(0))
            {
                isDragged = true;
            }
        }
        else if (isSelected)
        {
            myMat.color = selectCol;

            // deselect
            if (isHovered && Input.GetMouseButtonDown(0))
            {
                manager.SetActiveNode(null);
            }
        }
        else
        {
            myMat.color = defaultCol;
        }

        if (outputObject != null)
        {
            lineObject.transform.position = outputSphere.transform.position + Vector3.up * .25f;
            Vector3 position = (outputObject.inputSphere.transform.position - lineObject.transform.position) + Vector3.up * .25f;

            lineObject.SetPosition(1, position);
        }
    }

    private void UpdateNode()
    {
        nodeData.myName = myName;
        nodeData.myText = myText;
        nodeData.myId = myId;
        nodeData.myInputId = myInput;
        nodeData.myOutputId = myOutput;
        nodeData.position = transform.position;
    }

    private void OnMouseEnter()
    {
        isHovered = true;
    }

    private void OnMouseExit()
    {
        isHovered = false;
    }

    public void SetName(string newName)
    {
        myName = newName;
        nameObj.text = myName;
        UpdateNode();
    }

    public string GetName()
    {
        return myName;
    }

    public void SetText(string newText)
    {
        myText = newText;
        UpdateNode();
    }

    public string GetText()
    {
        return myText;
    }

    public void SetID(string newId)
    {
        myId = newId;
        UpdateNode();
    }

    public string GetID()
    {
        return myId;
    }

    public void SetInputID(string newId)
    {
        myInput = newId;
        UpdateNode();
    }

    public string GetInputID()
    {
        return myInput;
    }

    public void SetOutputID(string newId)
    {
        myOutput = newId;
        UpdateNode();
    }

    public string GetOutputID()
    {
        return myOutput;
    }

    public string GetLanguage()
    {
        return language.ToString();
    }

    public Node GetNode()
    {
        return nodeData;
    }

    public void CreateConnections(List<GameObject> nodes)
    {
        // assign output object properly based on ID, only used when loading
        foreach(GameObject node in nodes)
        {
            NodeObject data = node.GetComponent<NodeObject>();
            if (data.GetID() == myOutput)
            {
                outputObject = data;
                return;
            }
        }
    }
}

[Serializable]
public class Node
{
    public string myName = null;
    public string myText = null;
    public string myId = null;
    public string myInputId = null;
    public string myOutputId = null;

    public Vector3 position;
}

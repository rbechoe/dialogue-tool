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

    public TextMeshPro nameObj;

    // Trackables
    private Node nodeData;
    private string myName;
    private string myText;
    private string myId;
    private string myInput;
    private string myOutput;
    private Languages language = Languages.English;

    private void Awake()
    {
        nodeData = new Node();
    }

    void Start()
    {
        myMat = GetComponent<Renderer>().material;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ToolManager>();
        myMat.color = defaultCol;
    }

    void Update()
    {
        if (isHovered && !isSelected)
        {
            myMat.color = highlightCol;

            if (Input.GetMouseButtonUp(0))
            {
                manager.SetActiveNode(gameObject);
            }
        }
        else if (isSelected)
        {
            myMat.color = selectCol;
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
    public string myName;
    public string myText;
    public string myId;
    public string myInputId;
    public string myOutputId;

    public Vector3 position;
}

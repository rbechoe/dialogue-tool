using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private string myName;
    private string myText;
    private string myId;
    private string myInput;
    private string myOutput;
    private Languages language = Languages.English;

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
            Vector3 pos1 = outputSphere.transform.position + Vector3.up * .5f;
            Vector3 pos2 = outputObject.inputSphere.transform.position + Vector3.up * .5f;
            Vector3[] positions = new Vector3[] { pos1, pos2 };
            lineObject.SetPositions(positions);
        }
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
    }

    public string GetName()
    {
        return myName;
    }

    public void SetText(string newText)
    {
        myText = newText;
    }

    public string GetText()
    {
        return myText;
    }

    public void SetID(string newId)
    {
        myId = newId;
    }

    public string GetID()
    {
        return myId;
    }

    public void SetInputID(string newId)
    {
        myInput = newId;
    }

    public string GetInputID()
    {
        return myInput;
    }

    public void SetOutputID(string newId)
    {
        myOutput = newId;
    }

    public string GetOutputID()
    {
        return myOutput;
    }

    public string GetLanguage()
    {
        return language.ToString();
    }
}

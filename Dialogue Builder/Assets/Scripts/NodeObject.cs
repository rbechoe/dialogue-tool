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

    private Material myMat;
    private ToolManager manager;

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

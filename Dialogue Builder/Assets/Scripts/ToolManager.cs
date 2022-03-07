using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject cameraObj;
    public GameObject nodePrefab;
    public GameObject toolInfoWindow;
    public TextMeshProUGUI nodeName;
    public TextMeshProUGUI nodeInfo;
    public TMP_InputField nodeEditName;
    public TMP_InputField nodeEditText;

    private GameObject activeNode;
    private GameObject inputNode;
    private GameObject outputNode;

    private List<GameObject> nodes = new List<GameObject>();

    void Start()
    {
        // load latest save
    }

    private void Update()
    {
        if (inputNode != null && outputNode != null)
        {
            NodeObject inputObj = inputNode.GetComponent<NodeObject>();
            NodeObject outputObj = outputNode.GetComponent<NodeObject>();

            inputObj.inputObject = outputObj;
            outputObj.outputObject = inputObj;

            inputNode = null;
            outputNode = null;
        }
    }

    void UpdateSelectedNodes()
    {
        foreach (GameObject node in nodes)
        {
            if (node != activeNode)
            {
                node.GetComponent<NodeObject>().isSelected = false;
            }
            else
            {
                NodeObject nodeObj = node.GetComponent<NodeObject>();
                nodeObj.isSelected = true;
                toolInfoWindow.SetActive(true);
                nodeName.text = nodeObj.GetName();
                nodeInfo.text = "Node ID: " + nodeObj.GetID() +
                                "\nInput ID: " + nodeObj.GetInputID() +
                                "\nOutput ID: " + nodeObj.GetOutputID() +
                                "\nLanuage: " + nodeObj.GetLanguage();
                nodeEditName.text = nodeObj.GetName();
                nodeEditText.text = nodeObj.GetText();
            }
        }
    }

    public void CreateNode()
    {
        GameObject newNode = Instantiate(nodePrefab, cameraObj.transform.position - new Vector3(0, 10, 0), Quaternion.identity);
        nodes.Add(newNode);
        newNode.name = "Node " + nodes.Count;
        activeNode = newNode;

        NodeObject nodeObj = newNode.GetComponent<NodeObject>();
        nodeObj.SetName(newNode.name);
        nodeObj.SetID("" + nodes.Count);
        UpdateSelectedNodes();
    }

    public void SetActiveNode(GameObject nodeObject)
    {
        activeNode = nodeObject;
        UpdateSelectedNodes();
    }

    public void UpdateNodeInformation()
    {
        NodeObject nodeObj = activeNode.GetComponent<NodeObject>();
        nodeObj.SetName(nodeEditName.text);
        nodeObj.SetText(nodeEditText.text);
    }

    public void SetInputNode(GameObject node)
    {
        inputNode = node;
    }

    public void SetOutputNode(GameObject node)
    {
        outputNode = node;
    }
}

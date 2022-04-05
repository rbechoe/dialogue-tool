using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject cameraObj;
    public GameObject nodePrefab;
    public GameObject toolInfoWindow;
    public Transform hitObj;
    public Camera camera;
    public TextMeshProUGUI nodeName;
    public TextMeshProUGUI nodeInfo;
    public TMP_InputField nodeEditName;
    public TMP_InputField nodeEditText;

    private GameObject activeNode;
    private GameObject inputNode;
    private GameObject outputNode;

    private List<GameObject> nodes = new List<GameObject>();

    private DataSerializer dataSerializer;

    private void OnEnable()
    {
        EventSystem.AddListener(EventTypes.CreateNode, CreateNode);
    }

    private void OnDisable()
    {
        EventSystem.RemoveListener(EventTypes.CreateNode, CreateNode);
    }

    private void Start()
    {
        dataSerializer = gameObject.GetComponent<DataSerializer>();
    }

    private void Update()
    {
        LinkNodes();
        MoveHitObj();
    }

    private void MoveHitObj()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hitObj.position = hit.point;
        }
    }

    private void LinkNodes()
    {
        if (inputNode != null && outputNode != null)
        {
            NodeObject inputObj = inputNode.GetComponent<NodeObject>();
            NodeObject outputObj = outputNode.GetComponent<NodeObject>();

            inputObj.inputObject = outputObj;
            inputObj.SetInputID(outputObj.GetID());
            outputObj.outputObject = inputObj;
            outputObj.SetOutputID(inputObj.GetID());

            inputNode = null;
            outputNode = null;

            UpdateSelectedNodes();
        }
    }

    private void UpdateSelectedNodes()
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
        Vector3 spawnPos = new Vector3(hitObj.position.x, 0, hitObj.position.z);
        GameObject newNode = Instantiate(nodePrefab, spawnPos, Quaternion.identity);
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
        nodeName.text = nodeEditName.text;
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

    public void SaveNodes()
    {
        List<Node> nodeExports = new List<Node>();
        foreach(GameObject node in nodes)
        {
            nodeExports.Add(node.GetComponent<NodeObject>().GetNode());
        }

        dataSerializer.WriteSave(nodeExports);
    }

    public void ReadNodes()
    {
        List<Node> nodeCollection = dataSerializer.ReadSave();

        CleanEnvironment();

        // create all nodes
        foreach(Node node in nodeCollection)
        {
            GameObject newNode = Instantiate(nodePrefab, cameraObj.transform.position - new Vector3(0, 10, 0), Quaternion.identity);
            nodes.Add(newNode);
            NodeObject data = newNode.GetComponent<NodeObject>();

            // load in all world data
            newNode.transform.position = node.position;
            newNode.name = node.myName;

            // load in all node data
            data.SetText(node.myText);
            data.SetID(node.myId);
            data.SetInputID(node.myInputId);
            data.SetOutputID(node.myOutputId);
            data.SetName(node.myName);
        }

        // assign lines properly after all nodes have been loaded
        foreach (GameObject node in nodes)
        {
            NodeObject data = node.GetComponent<NodeObject>();
            data.CreateConnections(nodes);
        }

        print("Loaded save");
    }

    public void CleanEnvironment()
    {
        int listCount = nodes.Count;
        for (int i = 0; i < listCount; i++)
        {
            Destroy(nodes[0]);
            nodes.RemoveAt(0);
        }
    }

    public void UndoLatestAction()
    {

    }

    public void RedoAction()
    {

    }
}

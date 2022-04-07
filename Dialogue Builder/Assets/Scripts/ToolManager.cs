using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour
{
    [Header("Settings")]
    public float readingSpeed = 20;

    [Header("Assignables")]
    [SerializeField] private GameObject cameraObj;
    public GameObject nodePrefab;
    public GameObject toolInfoWindow;
    public GameObject dialogueBall;
    public Transform hitObj;
    public Camera camera;
    public TextMeshProUGUI nodeName;
    public TMP_InputField nodeEditName;
    public TMP_InputField nodeEditText;

    private GameObject activeNode;
    private GameObject inputNode;
    private GameObject outputNode;

    public GameObject dialogueBar;
    public TextMeshProUGUI dialogueText;
    public bool runningDialogue;

    private GameObject startNode;
    private GameObject endNode;
    private float totalDialogueTime;
    private float currentDialogueTime;

    private List<GameObject> nodes = new List<GameObject>();

    private DataSerializer dataSerializer;

    private static ToolManager instance;
    public static ToolManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        dataSerializer = gameObject.GetComponent<DataSerializer>();
        dialogueBar.SetActive(false);
        toolInfoWindow.SetActive(false);
        dialogueBall.SetActive(false);  
    }

    private void Update()
    {
        LinkNodes();
        MoveHitObj();
        MoveBall();
    }

    private void MoveBall()
    {
        if (!runningDialogue) return;

        currentDialogueTime += Time.deltaTime;
        float step = currentDialogueTime / totalDialogueTime;
        dialogueBall.transform.position = Vector3.Lerp(startNode.transform.position, endNode.transform.position, step);
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
                nodeEditName.text = nodeObj.GetName();
                nodeEditText.text = nodeObj.GetText();
            }
        }
    }

    public void SetActiveNode(GameObject nodeObject)
    {
        activeNode = nodeObject;
        UpdateSelectedNodes();

        // when empty value is passed then the object will be deselected
        if (nodeObject == null)
        {
            nodeName.text = "";
            nodeEditName.text = "";
            nodeEditText.text = "";
            toolInfoWindow.SetActive(false);
        }
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

    public void SaveNodes(string path = null)
    {
        List<Node> nodeExports = new List<Node>();
        foreach(GameObject node in nodes)
        {
            nodeExports.Add(node.GetComponent<NodeObject>().GetNode());
        }

        dataSerializer.WriteSave(nodeExports, path);
    }

    public void ReadNodes(string path = null)
    {
        List<Node> nodeCollection;

        if (path == null)
            nodeCollection = dataSerializer.ReadSave();
        else
            nodeCollection = dataSerializer.ReadSave(path);

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

    public void CreatedNode(GameObject node)
    {
        nodes.Add(node);
        node.GetComponent<NodeObject>().SetID("ID" + GameObject.FindGameObjectsWithTag("Node").Length);
    }

    public void RemovedNode(GameObject node)
    {
        nodes.Remove(node);
    }

    public void FireDialogues()
    {
        if (activeNode == null) return;
        NodeObject node = activeNode.GetComponent<NodeObject>();
        QueueDialogue(node);
    }

    private void QueueDialogue(NodeObject node, float waitStack = 0)
    {
        string[] info = { node.GetName(), node.GetText() };
        GameObject[] spheres;
        if (node.outputObject != null)
            spheres = new GameObject[]{ node.outputSphere, node.outputObject.inputSphere };
        else
            spheres = new GameObject[] { node.inputSphere, node.outputSphere };

        float charLength = (1f + node.GetText().Length / readingSpeed);
        float waitTime = charLength + waitStack;

        // use stack, because we want current line to fire immediately after previous line
        StartCoroutine(DelayedMethods<string[]>.DelayedMethod(FireLine, info, waitStack));
        StartCoroutine(DelayedMethods<GameObject[], float>.DelayedMethod(FireBall, spheres, charLength, waitStack));

        if (node.outputObject != null)
        {
            QueueDialogue(node.outputObject, waitTime);
        }
        else
        {
            StartCoroutine(DelayedMethods.DelayedMethod(DisableBar, waitTime));
        }
    }

    private void FireLine(string[] info)
    {
        dialogueBar.SetActive(true);
        runningDialogue = true;
        dialogueText.text = info[0] + ": " + info[1];
    }

    private void FireBall(GameObject[] objects, float time)
    {
        startNode = objects[0];
        endNode = objects[1];
        totalDialogueTime = time;
        currentDialogueTime = 0;
        dialogueBall.SetActive(true);
    }

    private void DisableBar()
    {
        dialogueBar.SetActive(false);
        runningDialogue = false;
        dialogueBall.SetActive(false);
    }
}

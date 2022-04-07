using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Windows.Forms;

public class CommandManager : MonoBehaviour
{
    public List<ICommand> commands = new List<ICommand>();

    private int commandPosition = -1;

    private static CommandManager instance;

    public static CommandManager Instance { get { return instance; } }

    private KeyCode commandCode;

    [Header("Accessibles")]
    public GameObject nodePrefab;


    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();
    [DllImport("user32.dll")]
    private static extern void SaveFileDialog();

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
        if (UnityEngine.Application.isEditor)
        {
            print("Running in editor! All control commands have been replaced with left shift!");
            commandCode = KeyCode.LeftShift;
        }
        else
        {
            commandCode = KeyCode.LeftControl;
        }
    }

    private void Update()
    {
        // save
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.S))
        {
            ToolManager.Instance.SaveNodes();
        }

        // save to
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.A))
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Json Files (*.json)|*.json";
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            if (filePath.Length == 0) return;

            ToolManager.Instance.SaveNodes(filePath);
        }

        // load
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.W))
        {
            ToolManager.Instance.ReadNodes();
        }

        // load from
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.D))
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Json Files (*.json)|*.json";
            dialog.ShowDialog();
            string filePath = dialog.FileName;
            if (filePath.Length == 0) return;

            ToolManager.Instance.ReadNodes(filePath);
        }

        // cleanup
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.Delete))
        {
            ToolManager.Instance.CleanEnvironment();
        }

        // play
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.P) && !ToolManager.Instance.runningDialogue)
        {
            ToolManager.Instance.FireDialogues();
        }

        // undo
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.Z))
        {
            StepThroughCommands(true);
        }

        // redo
        if (Input.GetKey(commandCode) && Input.GetKeyDown(KeyCode.Y))
        {
            StepThroughCommands(false);
        }

        // create node
        if (Input.GetKey(commandCode) && Input.GetMouseButtonDown(0))
        {
            AddCommand(new CreateNode());
        }

        // delete node
        if (Input.GetKey(commandCode) && Input.GetMouseButtonDown(1) && ToolManager.Instance.GetActiveNode() != null)
        {
            AddCommand(new DeleteNode());
        }
    }

    public void AddCommand(ICommand command)
    {
        while (commands.Count > commandPosition + 1)
        {
            commands.RemoveAt(commandPosition + 1);
        }

        commands.Add(command);
        commandPosition = commands.Count - 1;

        command.Execute();
    }

    public void StepThroughCommands(bool back)
    {
        if (back)
        {
            if (commandPosition < 0) return;
            commands[commandPosition].Undo();
            commandPosition--;
        }
        else
        {
            if (commandPosition + 2 > commands.Count) return;
            commands[commandPosition + 1].Redo();
            commandPosition++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<ICommand> commands = new List<ICommand>();

    private int commandPosition;

    private static CommandManager _instance;

    public static CommandManager Instance { get { return _instance; } }

    private KeyCode commandCode;

    [Header("Accessibles")]
    public GameObject nodePrefab;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
         if (Application.isEditor)
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
        // undo
        if (Input.GetKey(commandCode) && Input.GetKeyUp(KeyCode.Z))
        {
            StepThroughCommands(true);
        }

        // redo
        if (Input.GetKey(commandCode) && Input.GetKeyUp(KeyCode.Y))
        {
            StepThroughCommands(false);
        }

        // create node
        if (Input.GetKey(commandCode) && Input.GetMouseButtonUp(0))
        {
            AddCommand(new CreateNode());
        }
    }

    public void AddCommand(ICommand command)
    {
        int debugKill = 0;
        int count = commands.Count;
        while (commands.Count > commandPosition + 1)
        {
            commands.RemoveAt(commandPosition + 1);

            debugKill++;
            if (debugKill > count + 25)
            {
                print("killed while loop");
                break;
            }
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
            if (commandPosition > commands.Count) return;
            commands[commandPosition].Redo();
            commandPosition++;
        }
    }
}

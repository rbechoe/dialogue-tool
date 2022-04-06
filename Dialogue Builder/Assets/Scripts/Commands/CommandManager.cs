using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<ICommand> commands = new List<ICommand>();

    private int commandPosition = -1;

    private static CommandManager instance;

    public static CommandManager Instance { get { return instance; } }

    private KeyCode commandCode;

    [Header("Accessibles")]
    public GameObject nodePrefab;

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
            if (commandPosition + 2 > commands.Count) return;
            commands[commandPosition + 1].Redo();
            commandPosition++;
        }
    }
}

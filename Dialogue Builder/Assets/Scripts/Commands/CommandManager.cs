using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public List<ICommand> commands = new List<ICommand>();

    private int commandPosition;

    private static CommandManager _instance;

    public static CommandManager Instance { get { return _instance; } }

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
            commands[commandPosition].Undo();
            commandPosition--;
            if (commandPosition < 0)
            {
                commandPosition = 0;
            }
        }
        else
        {
            commands[commandPosition].Redo();
            commandPosition++;
            if (commandPosition >= commands.Count)
            {
                commandPosition = commands.Count - 1;
            }
        }
    }
}

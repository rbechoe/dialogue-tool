using UnityEngine;

public abstract class BaseCommand : MonoBehaviour, ICommand
{
    public object commandData; // used to store things such as position, text etc

    public abstract void Execute(ToolManager manager);
    public abstract void Redo();
    public abstract void Undo();
}

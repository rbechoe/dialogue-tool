using UnityEngine;

public abstract class BaseCommand : MonoBehaviour, ICommand
{
    public abstract void Execute();
    public abstract void Redo();
    public abstract void Undo();
}

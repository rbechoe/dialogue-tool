public interface ICommand
{
    void Execute(ToolManager manager);
    void Undo();
    void Redo();
}

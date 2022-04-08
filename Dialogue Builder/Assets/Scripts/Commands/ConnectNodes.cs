public class ConnectNodes : BaseCommand
{
    public NodeObject oldNode;
    public NodeObject newNode;
    public NodeObject curNode;

    public override void Execute()
    {
        curNode.outputObject = newNode;
    }

    public override void Redo()
    {
        curNode.outputObject = newNode;
    }

    public override void Undo()
    {
        curNode.outputObject = oldNode;
    }
}

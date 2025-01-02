namespace _Assets.Scripts.Services.Undo
{
    public interface IUndoableAction
    {
        void Execute();
        void Undo();
    }
}
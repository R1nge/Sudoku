using System.Collections.Generic;

namespace _Assets.Scripts.Services.Undo.Sudoku
{
    public class SudokuUndoHistory
    {
        private readonly Stack<IUndoableAction> _actionsStack = new(20);

        public void Do(IUndoableAction action)
        {
            action.Execute();
            _actionsStack.Push(action);
        }

        public void Undo()
        {
            if (_actionsStack.TryPop(out var action))
            {
                action.Undo();
            }
        }
    }
}
using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;

namespace _Assets.Scripts.Services.Undo.Sudoku
{
    public class SudokuResetNumbersAction : IUndoableAction
    {
        private readonly ISudokuCellView _cellView;
        private readonly SudokuGridModel _model;
        private readonly int _x, _y;
        private int _noteValue;
        private int _previousValue;

        public SudokuResetNumbersAction(SudokuGridModel model, int x, int y, ISudokuCellView cellView)
        {
            _model = model;
            _x = x;
            _y = y;
            _cellView = cellView;
        }

        public void Execute()
        {
            _previousValue = _model.GetCell(_x, _y).Number;
            _noteValue = _model.GetCell(_x, _y).NumberNote;
            _model.GetCell(_x, _y).SetNumber(0);
            _model.GetCell(_x, _y).SetNumberNote(0);
            _cellView.SetNumber(0);
        }

        public void Undo()
        {
            _model.GetCell(_x, _y).SetNumber(_previousValue);
            _model.GetCell(_x, _y).SetNumberNote(_noteValue);
            _cellView.SetNumbers(_previousValue, _noteValue);
        }
    }
}
using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;

namespace _Assets.Scripts.Services.Undo.Sudoku
{
    public class SudokuIncreaseNumberAction : IUndoableAction
    {
        private readonly ISudokuCellView _cellView;
        private readonly SudokuGridModel _model;
        private readonly int _x, _y;
        private int _previousValue;

        public SudokuIncreaseNumberAction(SudokuGridModel model, int x, int y, ISudokuCellView cellView)
        {
            _model = model;
            _x = x;
            _y = y;
            _cellView = cellView;
        }

        public void Execute()
        {
            _previousValue = _model.GetCell(_x, _y).Number;
            var number = _model.IncreaseNumber(_x, _y);
            _model.GetCell(_x, _y).SetNumber(number);
            _cellView.SetNumber(number);
        }

        public void Undo()
        {
            _model.SetNumber(_x, _y, _previousValue);
            _cellView.SetNumber(_previousValue);
        }
    }
}
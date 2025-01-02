using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;

namespace _Assets.Scripts.Services.Undo.Sudoku
{
    public class SudokuSetNumberAction : IUndoableAction
    {
        private readonly ISudokuCellView _cellView;
        private readonly SudokuGridModel _model;
        private readonly int _number;
        private readonly int _x, _y;
        private int _noteValue;
        private int _previousValue;

        public SudokuSetNumberAction(SudokuGridModel model, int x, int y, ISudokuCellView cellView, int number)
        {
            _model = model;
            _x = x;
            _y = y;
            _cellView = cellView;
            _number = number;
        }

        public void Execute()
        {
            _previousValue = _model.GetCell(_x, _y).Number;
            _noteValue = _model.GetCell(_x, _y).NumberNote;
            _model.GetCell(_x, _y).SetNumber(_number);
            _cellView.SetNumber(_number);
        }

        public void Undo()
        {
            _model.GetCell(_x, _y).SetNumber(_previousValue);
            _model.GetCell(_x, _y).SetNumberNote(_noteValue);
            _cellView.SetNumbers(_previousValue, _noteValue);
        }
    }
}
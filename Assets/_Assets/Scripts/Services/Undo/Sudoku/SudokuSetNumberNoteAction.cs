using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;

namespace _Assets.Scripts.Services.Undo.Sudoku
{
    public class SudokuSetNumberNoteAction : IUndoableAction
    {
        private readonly ISudokuCellView _cellView;
        private readonly SudokuGridModel _model;
        private readonly int _number;
        private readonly int _x, _y;
        private int _numberValue;
        private int _previousValue;

        public SudokuSetNumberNoteAction(SudokuGridModel model, int x, int y, ISudokuCellView cellView, int number)
        {
            _model = model;
            _x = x;
            _y = y;
            _cellView = cellView;
            _number = number;
        }

        public void Execute()
        {
            _previousValue = _model.GetCell(_x, _y).NumberNote;
            _numberValue = _model.GetCell(_x, _y).Number;
            _model.GetCell(_x, _y).Number = 0;
            _model.GetCell(_x, _y).SetNumberNote(_number);
            _cellView.SetNumberNote(_number);
        }

        public void Undo()
        {
            _model.GetCell(_x, _y).SetNumber(_numberValue);
            _model.GetCell(_x, _y).SetNumberNote(_previousValue);
            _cellView.SetNumbers(_numberValue, _previousValue);
        }
    }
}
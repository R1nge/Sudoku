using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;
using _Assets.Scripts.Services.Undo.Sudoku;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers
{
    public class SudokuGridController
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;
        private readonly Sudoku _sudoku;
        private SudokuGridModel _gridModel;
        private SudokuGridView _gridView;
        private SudokuSelectionView _sudokuSelectionView;
        private SudokuUndoHistory _sudokuUndoHistory;

        public SudokuGridController(Sudoku sudoku, IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _sudoku = sudoku;
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public void Init(SudokuGridView sudokuGridView)
        {
            _sudokuUndoHistory = new SudokuUndoHistory();

            const int width = 9;
            const int height = 9;

            _gridModel = new SudokuGridModel(width, height);
            _gridView = sudokuGridView;

            var board = _sudoku.Generate(width, height);

            for (int y = 0; y < board.GetLength(1); y++)
            {
                for (int x = 0; x < board.GetLength(0); x++)
                {
                    _gridModel.SetNumber(x, y, board[x, y]);
                }
            }


            _gridView.Init(_gridModel);

            _sudokuSelectionView =
                _objectResolver.Instantiate(_configProvider.SudokuSelectionView, sudokuGridView.transform);
            _sudokuSelectionView.Hide();
        }

        public void Undo()
        {
            _sudokuUndoHistory.Undo();
        }

        public void HideSelection()
        {
            _sudokuSelectionView.Hide();
        }

        public void SetNumber(ISudokuCellView sudokuView, int number)
        {
            var x = sudokuView.X;
            var y = sudokuView.Y;
            if (_gridModel.Cells[x, y].IsChangeable)
            {
                _sudokuUndoHistory.Do(new SudokuSetNumberAction(_gridModel, x, y, sudokuView, number));
                _sudokuSelectionView.Hide();
                CheckWin();
            }
        }

        public void SetNumberNote(ISudokuCellView sudokuView, int number)
        {
            var x = sudokuView.X;
            var y = sudokuView.Y;
            if (_gridModel.Cells[x, y].IsChangeable)
            {
                _sudokuUndoHistory.Do(new SudokuSetNumberNoteAction(_gridModel, x, y, sudokuView, number));
                _sudokuSelectionView.Hide();
            }
        }

        public void Reset(ISudokuCellView sudokuView)
        {
            var x = sudokuView.X;
            var y = sudokuView.Y;
            if (_gridModel.Cells[x, y].IsChangeable)
            {
                _sudokuUndoHistory.Do(new SudokuSetNumberAction(_gridModel, x, y, sudokuView, 0));
                _sudokuSelectionView.Hide();
                CheckWin();
            }
        }

        private void CheckWin()
        {
            if (_sudoku.CheckWin(_gridModel.ToIntArray()))
            {
                Debug.LogError("Sudoku WIN");
            }
            else
            {
                Debug.LogError("Sudoku NOT WIN");
            }
        }

        public void ShowSelection(ISudokuCellView sudokuView)
        {
            var x = sudokuView.X;
            var y = sudokuView.Y;
            if (_gridModel.Cells[x, y].IsChangeable)
            {
                _sudokuSelectionView.Show(sudokuView);
            }
        }
    }
}
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;
using _Assets.Scripts.Services.Lives;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.Undo.Sudoku;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers
{
    public class SudokuGridController
    {
        private readonly ConfigProvider _configProvider;
        private readonly GameStateMachine _gameStateMachine;
        private readonly LivesHolder _livesHolder;
        private readonly IObjectResolver _objectResolver;
        private readonly Sudoku _sudoku;
        private SudokuGridModel _gridModel;
        private SudokuGridView _gridView;
        private SudokuSelectionView _sudokuSelectionView;
        private SudokuUndoHistory _sudokuUndoHistory;

        public SudokuGridController(Sudoku sudoku, IObjectResolver objectResolver, ConfigProvider configProvider,
            LivesHolder livesHolder, GameStateMachine gameStateMachine)
        {
            _sudoku = sudoku;
            _objectResolver = objectResolver;
            _configProvider = configProvider;
            _livesHolder = livesHolder;
            _gameStateMachine = gameStateMachine;
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
                    _gridModel.Init(x, y, board[x, y]);
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
                if (!HasErrors(x, y))
                {
                    _gridModel.Cells[x, y].SetChangeable(false);
                    _gridView.PlacedCorrectly(x, y);
                    _sudokuUndoHistory.Clear();
                    if (!CheckWin())
                    {
                        _gridView.HighlightSubGrid(x, y);
                        _gridView.HighlightCellsWith(number);
                    }
                }
                else
                {
                    _livesHolder.DecreaseLives();
                    if (!_livesHolder.HasLives)
                    {
                        _gameStateMachine.SwitchState(GameStateType.Lose).Forget();
                    }
                }
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
                if (_gridModel.Cells[x, y].Number != 0)
                {
                    _sudokuUndoHistory.Do(new SudokuResetNumbersAction(_gridModel, x, y, sudokuView));
                    _sudokuSelectionView.Hide();
                }
            }
        }

        private bool CheckWin()
        {
            if (_sudoku.CheckWin(_gridModel.ToIntArray()))
            {
                Debug.LogError("Sudoku WIN");
                return true;
            }

            Debug.LogError("Sudoku NOT WIN");

            return false;
        }

        private bool HasErrors(int x, int y)
        {
            if (_sudoku.HasCellError(_gridModel.ToIntArray(), x, y))
            {
                Debug.LogError("Cell error");
                return true;
            }

            return false;
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

        public void Highlight(int number, int x, int y)
        {
            _gridView.ResetHighlight();

            _gridView.HighlightHorizontalRows(y);
            _gridView.HighlightVerticalRows(x);
            _gridView.HighlightSubGrid(x, y);

            if (number == 0)
                return;

            _gridView.HighlightCellsWith(number);
        }

        public void ResetHighlight()
        {
            _gridView.ResetHighlight();
        }

        public void OpenMostObviousCell()
        {
            var spot = _sudoku.FindMostObviousSpot(_gridModel.ToIntArray(), 10);
            if (spot != null)
            {
                _gridModel.GetCell(spot.X, spot.Y).SetNumber(spot.Number);
                _gridView.GetCellView(spot.X, spot.Y).SetNumber(spot.Number);
            }
        }

        public void Dispose()
        {
            Object.Destroy(_gridView.gameObject);
            Object.Destroy(_sudokuSelectionView.gameObject);
            _gridModel = null;
            _gridView = null;
            _sudokuUndoHistory = null;
        }
    }
}
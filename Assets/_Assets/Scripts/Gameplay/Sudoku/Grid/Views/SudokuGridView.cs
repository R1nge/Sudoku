using _Assets.Scripts.Gameplay.Sudoku.Grid.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Views
{
    public class SudokuGridView : MonoBehaviour
    {
        [SerializeField] private GameObject cellView;
        [SerializeField] private Transform cellsParent;
        private ISudokuCellView[,] _cells;
        [Inject] private IObjectResolver _objectResolver;

        public void Init(SudokuGridModel sudokuGridModel)
        {
            if (_cells != null)
            {
                for (int y = _cells.GetLength(1) - 1; y >= 0; y--)
                {
                    for (int x = _cells.GetLength(0) - 1; x >= 0; x--)
                    {
                        Destroy(_cells[x, y].GameObject);
                    }
                }
            }

            _cells = new ISudokuCellView[sudokuGridModel.Width, sudokuGridModel.Height];
            for (int y = 0; y < sudokuGridModel.Height; y++)
            {
                for (int x = 0; x < sudokuGridModel.Width; x++)
                {
                    var cell = _objectResolver.Instantiate(cellView, cellsParent);
                    _cells[x, y] = cell.GetComponent<ISudokuCellView>();
                    var cellData = sudokuGridModel.Cells[x, y];
                    _cells[x, y].Init(x, y, cellData.Number);
                }
            }


            for (int y = 0; y < sudokuGridModel.Height; y++)
            {
                for (int x = 0; x < sudokuGridModel.Width; x++)
                {
                    _cells[x, y].GameObject.transform.localPosition = new Vector3(x * 50, y * 50, 0);
                }

                cellsParent.transform.localPosition = new Vector3((-sudokuGridModel.Width + 1) * 25,
                    (-sudokuGridModel.Height + 1) * 25, 0);
            }
        }

        public ISudokuCellView GetCellView(int x, int y) => _cells[x, y];

        public void HighlightCellsWith(int number)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                for (int x = 0; x < _cells.GetLength(0); x++)
                {
                    if (_cells[x, y].Number == number)
                    {
                        _cells[x, y].Highlight(true);
                    }
                }
            }
        }

        public void HighlightVerticalRows(int x)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                _cells[x, y].Highlight(true);
            }
        }

        public void HighlightHorizontalRows(int y)
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                _cells[x, y].Highlight(true);
            }
        }

        public void HighlightSubGrid(int x, int y)
        {
            int startRow = x / 3 * 3;
            int startCol = y / 3 * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _cells[startRow + i, startCol + j].Highlight(true);
                }
            }
        }

        public void ResetHighlight()
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                for (int x = 0; x < _cells.GetLength(0); x++)
                {
                    _cells[x, y].Highlight(false);
                }
            }
        }

        public void PlacedCorrectly(int x, int y)
        {
            _cells[x, y].PlacedCorrectly();
        }
    }
}
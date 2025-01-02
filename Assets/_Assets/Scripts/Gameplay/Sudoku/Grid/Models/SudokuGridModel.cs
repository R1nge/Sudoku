using UnityEngine;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Models
{
    public class SudokuGridModel
    {
        public SudokuCellModel[,] Cells;

        public SudokuGridModel(int width, int height)
        {
            Cells = new SudokuCellModel[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Cells[x, y] = new SudokuCellModel();
                    Cells[x, y].SetPosition(x, y);
                }
            }
        }

        public int Width => Cells.GetLength(0);
        public int Height => Cells.GetLength(1);

        public void Init(int x, int y, int number)
        {
            Cells[x, y].SetNumber(number);
            Cells[x, y].SetChangeable(number == 0);
        }

        public int[,] ToIntArray()
        {
            var arr = new int[Cells.GetLength(0), Cells.GetLength(1)];
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    arr[x, y] = Cells[x, y].Number;
                }
            }

            return arr;
        }

        public SudokuCellModel GetCell(int x, int y)
        {
            return Cells[x, y];
        }
    }
}
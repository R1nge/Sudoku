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

        public void SetNumber(int x, int y, int number)
        {
            if (number != 0)
            {
                Cells[x, y].IsChangeable = false;
            }
            else
            {
                Cells[x, y].IsChangeable = true;
            }

            Cells[x, y].SetNumber(number);
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

        public int IncreaseNumber(int x, int y)
        {
            if (Cells[x, y].IsChangeable)
            {
                var nextNumber = (Cells[x, y].Number + 1) % 10;

                if (nextNumber == 0)
                {
                    nextNumber = 1;
                }

                Cells[x, y].SetNumber(nextNumber);
            }

            return Cells[x, y].Number;
        }

        public int DecreaseNumber(int x, int y)
        {
            if (Cells[x, y].IsChangeable)
            {
                var prevNumber = (Cells[x, y].Number - 1 + 10) % 10;

                if (prevNumber == 0)
                {
                    prevNumber = 9;
                }

                Cells[x, y].SetNumber(prevNumber);
            }

            return Cells[x, y].Number;
        }

        public SudokuCellModel GetCell(int x, int y)
        {
            return Cells[x, y];
        }
    }
}
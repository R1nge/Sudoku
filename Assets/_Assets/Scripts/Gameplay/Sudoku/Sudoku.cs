using System;
using System.Collections.Generic;
using System.Linq;

namespace _Assets.Scripts.Gameplay.Sudoku
{
    public class Sudoku
    {
        private readonly Random _random = new();

        public int[,] Generate(int width, int height)
        {
            var grid = new int[width, height];

            for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                grid[i, j] = 0;

            Solve(grid);
            MakeUnique(grid);

            return grid;
        }

        private bool IsValid(int[,] sudokuBoard, (int row, int column) cellPosition)
        {
            var (row, column) = cellPosition;
            int value = sudokuBoard[column, row];
            if (value == 0) return true;
            bool isUniqueInColumn = SudokuHelper.GetVerticalNeighbours(column)
                .Select(((int r, int c) cell) => sudokuBoard[cell.c, cell.r]).Count(v => v == value) == 1;
            bool isUniqueInRow = SudokuHelper.GetHorizontalNeighbours(row)
                .Select(((int r, int c) cell) => sudokuBoard[cell.c, cell.r]).Count(v => v == value) == 1;
            bool isUniqueInSquare = SudokuHelper.GetWithinSquareNeighbours(row, column)
                .Select(((int r, int c) cell) => sudokuBoard[cell.c, cell.r]).Count(v => v == value) == 1;
            return isUniqueInColumn && isUniqueInRow && isUniqueInSquare;
        }

        public bool IsValidBoard(int[,] sudokuBoard)
        {
            if (SudokuHelper.HasIncorrectDimensions(sudokuBoard)) return false;
            for (int y = 0; y < 9; y++)
            for (int x = 0; x < 9; x++)
                if (!IsValid(sudokuBoard, (y, x)))
                    return false;
            return true;
        }

        public bool CheckWin(int[,] sudokuBoard)
        {
            HashSet<int> checkSet = new HashSet<int>();

            for (int i = 0; i < 9; i++)
            {
                checkSet.Clear();
                for (int j = 0; j < 9; j++)
                {
                    if (!checkSet.Add(sudokuBoard[i, j]))
                        return false;
                }

                checkSet.Clear();
                for (int j = 0; j < 9; j++)
                {
                    if (!checkSet.Add(sudokuBoard[j, i]))
                        return false;
                }
            }

            for (int row = 0; row < 9; row += 3)
            {
                for (int col = 0; col < 9; col += 3)
                {
                    checkSet.Clear();
                    for (int i = row; i < row + 3; i++)
                    {
                        for (int j = col; j < col + 3; j++)
                        {
                            if (!checkSet.Add(sudokuBoard[i, j]))
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void Solve(int[,] grid)
        {
            var guessArray = Enumerable.Range(1, 9).OrderBy(o => _random.Next()).ToArray();
            BackTracking(grid, guessArray);
        }

        private bool BackTracking(int[,] grid, int[] guessArray)
        {
            int row = 0, col = 0;

            if (!FindEmptyLocation(grid, ref row, ref col))
                return true;

            for (int num = 0; num < 9; num++)
            {
                if (IsSafe(grid, row, col, guessArray[num]))
                {
                    grid[row, col] = guessArray[num];

                    if (BackTracking(grid, guessArray))
                        return true;

                    grid[row, col] = 0;
                }
            }

            return false;
        }

        private bool FindEmptyLocation(int[,] grid, ref int row, ref int col)
        {
            for (row = 0; row < 9; row++)
            for (col = 0; col < 9; col++)
                if (grid[row, col] == 0)
                    return true;
            return false;
        }

        private bool UsedInRow(int[,] grid, int row, int value)
        {
            for (int i = 0; i < 9; i++)
                if (grid[row, i] == value)
                    return true;
            return false;
        }

        private bool UsedInCol(int[,] grid, int col, int value)
        {
            for (int i = 0; i < 9; i++)
                if (grid[i, col] == value)
                    return true;
            return false;
        }

        private bool UsedInBox(int[,] grid, int boxStartRow, int boxStartCol, int value)
        {
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (grid[i + boxStartRow, j + boxStartCol] == value)
                    return true;
            return false;
        }

        private bool IsSafe(int[,] grid, int row, int col, int value)
        {
            return !UsedInRow(grid, row, value) && !UsedInCol(grid, col, value) &&
                   !UsedInBox(grid, row - row % 3, col - col % 3, value);
        }

        private void MakeUnique(int[,] grid)
        {
            var randomIndexes = Enumerable.Range(0, 81).OrderBy(o => _random.Next()).ToArray();
            var guessArray = Enumerable.Range(1, 9).OrderBy(o => _random.Next()).ToArray();

            for (int i = 0; i < 81; i++)
            {
                int x = randomIndexes[i] / 9;
                int y = randomIndexes[i] % 9;
                int temp = grid[x, y];
                //This makes a cell empty
                grid[x, y] = 0;

                int check = 0;
                CheckUniqueness(grid, guessArray, ref check);
                //This check for gaps?
                if (check != 1)
                {
                    grid[x, y] = temp;
                }
            }
        }

        private void CheckUniqueness(int[,] grid, int[] guessArray, ref int number)
        {
            int row = 0, col = 0;

            if (!FindEmptyLocation(grid, ref row, ref col))
            {
                number++;
                return;
            }

            for (int i = 0; i < 9 && number < 2; i++)
            {
                if (IsSafe(grid, row, col, guessArray[i]))
                {
                    grid[row, col] = guessArray[i];
                    CheckUniqueness(grid, guessArray, ref number);
                }

                grid[row, col] = 0;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace _Assets.Scripts.Gameplay.Sudoku
{
    public static class SudokuHelper
    {
        public static IEnumerable<(int, int)> GetWithinSquareNeighbours(int row, int column)
        {
            return Enumerable.Range(row / 3 * 3, 3)
                .Join(Enumerable.Range(column / 3 * 3, 3), i => 0, i => 0, (r, c) => (r, c));
        }

        public static IEnumerable<(int, int)> GetVerticalNeighbours(int column)
        {
            return Enumerable.Range(0, 9)
                .Zip(Enumerable.Repeat(column, 9), (r, c) => (r, c));
        }

        public static IEnumerable<(int, int)> GetHorizontalNeighbours(int row)
        {
            return Enumerable.Repeat(row, 9)
                .Zip(Enumerable.Range(0, 9), (r, c) => (r, c));
        }

        public static bool HasIncorrectDimensions<T>(T[,] board)
        {
            return board.Rank != 2 || board.GetLength(0) != 9 || board.GetLength(1) != 9;
        }
    }
}
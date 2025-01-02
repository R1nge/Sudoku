using System;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Models
{
    [Serializable]
    public class SudokuCellModel
    {
        public int X;
        public int Y;
        public int Number;
        public int NumberNote;
        public bool IsChangeable;

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetNumber(int number) => Number = number;

        public void SetNumberNote(int number) => NumberNote = number;

        public void SetChangeable(bool isChangeable) => IsChangeable = isChangeable;
    }
}
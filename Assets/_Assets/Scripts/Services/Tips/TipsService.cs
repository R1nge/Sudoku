using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Tips
{
    public class TipsService : ITickable
    {
        private readonly SudokuGridController _sudokuGridController;

        private TipsService(SudokuGridController sudokuGridController)
        {
            _sudokuGridController = sudokuGridController;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _sudokuGridController.OpenMostObviousCell();
            }
        }

        public void Show()
        {
        }

        public void NextStep()
        {
        }
    }
}
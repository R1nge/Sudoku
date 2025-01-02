using _Assets.Scripts.Gameplay.Sudoku;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using _Assets.Scripts.Services.Grid;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using CanvasScaler = _Assets.Scripts.Misc.CanvasScaler;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SudokuState : IAsyncState
    {
        private readonly CanvasScaler _canvasScaler;
        private readonly GridViewFactory _gridViewFactory;
        private readonly SudokuGridController _sudokuGridController;
        private readonly SudokuPlayerInput _sudokuPlayerInput;

        public SudokuState(SudokuGridController sudokuGridController, GridViewFactory gridViewFactory,
            SudokuPlayerInput sudokuPlayerInput, CanvasScaler canvasScaler)
        {
            _sudokuGridController = sudokuGridController;
            _gridViewFactory = gridViewFactory;
            _sudokuPlayerInput = sudokuPlayerInput;
            _canvasScaler = canvasScaler;
        }

        public async UniTask Enter()
        {
            var parent = GameObject.Find("GameUI(Clone)").transform;
            _sudokuPlayerInput.Init(parent.GetComponent<GraphicRaycaster>(), _sudokuGridController);
            var gridView = _gridViewFactory.CreateSudoku(parent);
            _canvasScaler.Init((RectTransform)gridView.transform);
            _sudokuGridController.Init(gridView);

            await UniTask.DelayFrame(1);
            _sudokuPlayerInput.Enable();
            _canvasScaler.Enable();
        }

        public async UniTask Exit()
        {
            _canvasScaler.Disable();
        }
    }
}
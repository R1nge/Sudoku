using System;
using _Assets.Scripts.Gameplay.Sudoku;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Grid;
using _Assets.Scripts.Services.StateMachine.States;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine
{
    public class MainMenuStatesFactory
    {
        private readonly CanvasScaler _canvasScaler;
        private readonly GridViewFactory _gridViewFactory;
        private readonly SudokuGridController _sudokuGridController;
        private readonly SudokuPlayerInput _sudokuPlayerInput;
        private readonly UIStateMachine _uiStateMachine;

        private MainMenuStatesFactory(UIStateMachine uiStateMachine,
            GridViewFactory gridViewFactory, SudokuGridController sudokuGridController,
            SudokuPlayerInput sudokuPlayerInput, CanvasScaler canvasScaler)
        {
            _uiStateMachine = uiStateMachine;
            _gridViewFactory = gridViewFactory;
            _sudokuGridController = sudokuGridController;
            _sudokuPlayerInput = sudokuPlayerInput;
            _canvasScaler = canvasScaler;
        }

        public IAsyncState CreateAsyncState(GameStateType gameStateType, GameStateMachine gameStateMachine)
        {
            switch (gameStateType)
            {
                case GameStateType.Init:
                    return new InitState(gameStateMachine, _uiStateMachine);
                case GameStateType.Sudoku:
                    return new SudokuState(_sudokuGridController, _gridViewFactory, _sudokuPlayerInput, _canvasScaler);
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameStateType), gameStateType, null);
            }
        }
    }
}
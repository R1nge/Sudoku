using _Assets.Scripts.Services.StatesCreator;
using _Assets.Scripts.Services.UIs.StateMachine;

namespace _Assets.Scripts.Services.StateMachine.StatesCreators
{
    public class MainSceneStateCreator : IStateCreator
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly MainMenuStatesFactory _mainMenuStatesFactory;
        private readonly MainMenuUIStatesFactory _mainMenuUIStatesFactory;
        private readonly UIStateMachine _uiStateMachine;

        private MainSceneStateCreator(GameStateMachine gameStateMachine, UIStateMachine uiStateMachine,
            MainMenuStatesFactory mainMenuStatesFactory, MainMenuUIStatesFactory mainMenuUIStatesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiStateMachine = uiStateMachine;
            _mainMenuStatesFactory = mainMenuStatesFactory;
            _mainMenuUIStatesFactory = mainMenuUIStatesFactory;
        }

        public void Init()
        {
            _gameStateMachine.AddState(GameStateType.Init,
                _mainMenuStatesFactory.CreateAsyncState(GameStateType.Init, _gameStateMachine));
            _gameStateMachine.AddState(GameStateType.Sudoku,
                _mainMenuStatesFactory.CreateAsyncState(GameStateType.Sudoku, _gameStateMachine));
            _gameStateMachine.AddState(GameStateType.Lose,
                _mainMenuStatesFactory.CreateAsyncState(GameStateType.Lose, _gameStateMachine));

            _uiStateMachine.AddState(UIStateType.Loading,
                _mainMenuUIStatesFactory.CreateState(UIStateType.Loading, _uiStateMachine));
            _uiStateMachine.AddState(UIStateType.Game,
                _mainMenuUIStatesFactory.CreateState(UIStateType.Game, _uiStateMachine));
            _uiStateMachine.AddState(UIStateType.Lose,
                _mainMenuUIStatesFactory.CreateState(UIStateType.Lose, _uiStateMachine));
        }

        public void Dispose()
        {
            _gameStateMachine.RemoveState(GameStateType.Init);
            _gameStateMachine.RemoveState(GameStateType.Sudoku);
            _gameStateMachine.RemoveState(GameStateType.Lose);

            _uiStateMachine.RemoveState(UIStateType.Loading);
            _uiStateMachine.RemoveState(UIStateType.Game);
            _uiStateMachine.RemoveState(UIStateType.Lose);
        }
    }
}
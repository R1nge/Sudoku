using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class SudokuLoseState : IAsyncState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly UIStateMachine _uiStateMachine;

        public SudokuLoseState(GameStateMachine stateMachine, UIStateMachine uiStateMachine)
        {
            _stateMachine = stateMachine;
            _uiStateMachine = uiStateMachine;
        }

        public async UniTask Enter()
        {
            await _uiStateMachine.SwitchState(UIStateType.Lose);
        }

        public async UniTask Exit()
        {
        }
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs.StateMachine.States
{
    public class UILoseState : IAsyncState
    {
        private readonly MainMenuUIFactory _mainMenuUIFactory;
        private GameObject _ui;

        public UILoseState(MainMenuUIFactory mainMenuUIFactory) => _mainMenuUIFactory = mainMenuUIFactory;

        public async UniTask Enter() => _ui = _mainMenuUIFactory.CreateUI(UIStateType.Lose);

        public async UniTask Exit() => Object.Destroy(_ui);
    }
}
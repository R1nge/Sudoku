using _Assets.Scripts.Gameplay.Camera;
using _Assets.Scripts.Gameplay.Sudoku;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Grid;
using _Assets.Scripts.Services.Lives;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.StateMachine.StatesCreators;
using _Assets.Scripts.Services.Tips;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using _Assets.Scripts.Services.UIs.StatesCreators;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class MainSceneInstaller : LifetimeScope
    {
        [SerializeField] private CameraHandler cameraHandler;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LivesHolder>(Lifetime.Singleton);

            builder.RegisterComponent(cameraHandler);
            builder.RegisterEntryPoint<CanvasScaler>().AsSelf();
            builder.RegisterEntryPoint<CameraZoomer>().AsSelf();

            Sudoku(builder);
            builder.Register<GridViewFactory>(Lifetime.Singleton);

            builder.Register<MainMenuUIStatesFactory>(Lifetime.Singleton);
            builder.Register<MainMenuUIFactory>(Lifetime.Singleton);
            builder.Register<MainMenuStatesFactory>(Lifetime.Singleton);

            builder.Register<UIMainSceneStateCreator>(Lifetime.Singleton);
            builder.Register<MainSceneStateCreator>(Lifetime.Singleton);
        }


        private void Sudoku(IContainerBuilder builder)
        {
            builder.Register<Sudoku>(Lifetime.Singleton);
            builder.RegisterEntryPoint<SudokuPlayerInput>().AsSelf();
            builder.Register<SudokuGridController>(Lifetime.Singleton);
            builder.Register<TipsService>(Lifetime.Singleton).AsSelf();
        }
    }
}
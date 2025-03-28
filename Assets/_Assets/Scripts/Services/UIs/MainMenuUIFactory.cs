﻿using System;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.UIs
{
    public class MainMenuUIFactory
    {
        private readonly ConfigProvider _configProvider;
        private readonly IObjectResolver _objectResolver;

        public MainMenuUIFactory(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public GameObject CreateUI(UIStateType uiStateType)
        {
            switch (uiStateType)
            {
                case UIStateType.None:
                    break;
                case UIStateType.Loading:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.LoadingUI);
                case UIStateType.Game:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.GameUI);
                case UIStateType.Lose:
                    return _objectResolver.Instantiate(_configProvider.UIConfig.LoseUI);
                default:
                    throw new ArgumentOutOfRangeException(nameof(uiStateType), uiStateType, null);
            }

            return null;
        }
    }
}
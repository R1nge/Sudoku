using System;
using _Assets.Scripts.Services.Lives;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Services.UIs
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI livesText;
        [Inject] private LivesHolder _livesHolder;

        private void Awake()
        {
            _livesHolder.OnLivesChanged += UpdateLives;
            UpdateLives(_livesHolder.Lives);
        }

        private void UpdateLives(int lives)
        {
            livesText.text = lives.ToString();
        }

        private void OnDestroy()
        {
            _livesHolder.OnLivesChanged -= UpdateLives;
        }
    }
}
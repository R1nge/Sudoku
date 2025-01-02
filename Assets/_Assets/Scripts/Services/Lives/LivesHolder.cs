using System;

namespace _Assets.Scripts.Services.Lives
{
    public class LivesHolder
    {
        private int _lives;

        private LivesHolder() => Lives = 3;

        public int Lives
        {
            get => _lives;
            private set
            {
                _lives = value;
                OnLivesChanged?.Invoke(_lives);
            }
        }

        public bool HasLives => Lives > 0;
        public event Action<int> OnLivesChanged;
        public void DecreaseLives() => Lives--;
        public void IncreaseLives() => Lives++;
        public void ResetLives() => Lives = 3;
    }
}
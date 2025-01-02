using _Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Views
{
    public class SudokuSkinView : MonoBehaviour, ISudokuCellView
    {
        [SerializeField] private Color note;
        [SerializeField] private Image image;
        [Inject] private ConfigProvider _configProvider;
        private Color _defaultColor;

        private int _numberNote;

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Number { get; private set; }
        public GameObject GameObject => gameObject;

        public void Init(int x, int y, int number)
        {
            _defaultColor = image.color;
            X = x;
            Y = y;
            Number = number;
            SetSprite(number);
        }

        public void SetNumber(int number)
        {
            Number = number;
            _numberNote = 0;
            SetSprite(number);
        }

        public void SetNumberNote(int number)
        {
            _numberNote = number;
            Number = 0;
            SetSprite(number);
        }

        public void SetNumbers(int value, int note)
        {
            Number = value;
            _numberNote = note;

            if (Number != 0)
            {
                SetSprite(Number);
                image.color = _defaultColor;
            }
            else if (_numberNote != 0)
            {
                SetSprite(_numberNote);
                image.color = this.note;
            }
            else
            {
                SetSprite(0);
                image.color = _defaultColor;
            }
        }

        private void SetSprite(int number)
        {
            if (number <= 0)
            {
                image.sprite = null;
            }
            else
            {
                image.sprite = _configProvider.SudokuSkin.Sprites[number];
            }

            if (_numberNote != 0)
            {
                image.color = note;
            }
            else
            {
                image.color = _defaultColor;
            }
        }
    }
}
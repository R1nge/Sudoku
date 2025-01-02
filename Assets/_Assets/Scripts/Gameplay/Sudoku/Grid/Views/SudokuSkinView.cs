using _Assets.Scripts.Configs;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Views
{
    public class SudokuSkinView : MonoBehaviour, ISudokuCellView
    {
        [SerializeField] private Color highLightColor;
        [SerializeField] private Image image;
        [SerializeField] private Image[] notes;
        [Inject] private ConfigProvider _configProvider;

        private Color _defaultColor;
        private bool _isPlacedCorrectly;
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
            SetSpriteInit(number);

            for (int i = 0; i < notes.Length; i++)
            {
                notes[i].sprite = _configProvider.SudokuSkin.Sprites[i];
                notes[i].gameObject.SetActive(false);
            }
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
            SetSpriteNote(number);
        }

        public void SetNumbers(int value, int note)
        {
            Number = value;
            _numberNote = note;

            if (Number != 0 && _numberNote != 0)
            {
                SetSprite(Number);
            }
            else if (Number != 0 && _numberNote == 0)
            {
                SetSprite(Number);
            }
            else if (Number == 0 && _numberNote != 0)
            {
                SetSprite(0);
                SetSpriteNote(_numberNote);
            }
            else
            {
                SetSprite(0);
            }
        }

        public void Highlight(bool highlight)
        {
            if (_isPlacedCorrectly)
            {
                return;
            }

            if (highlight)
            {
                image.color = highLightColor;
            }
            else
            {
                image.color = _defaultColor;
            }
        }

        public void PlacedCorrectly()
        {
            _isPlacedCorrectly = true;
            image.color = Color.green;
        }

        private void SetSpriteInit(int number)
        {
            ResetNoteSprites();

            if (number == 0)
            {
                image.sprite = null;
                return;
            }

            number--;

            image.sprite = _configProvider.SudokuSkin.Sprites[number];
        }

        private void SetSprite(int number)
        {
            ResetNoteSprites();

            if (number <= 0)
            {
                image.sprite = null;
            }
            else
            {
                image.sprite = _configProvider.SudokuSkin.Sprites[number - 1];
            }
        }

        private void SetSpriteNote(int number)
        {
            ResetNoteSprites();

            if (number <= 0)
            {
                ResetNoteSprites();
            }
            else
            {
                notes[number - 1].gameObject.SetActive(true);
            }

            image.sprite = null;
        }

        private void ResetNoteSprites()
        {
            for (int i = 0; i < notes.Length; i++)
            {
                notes[i].gameObject.SetActive(false);
            }
        }
    }
}
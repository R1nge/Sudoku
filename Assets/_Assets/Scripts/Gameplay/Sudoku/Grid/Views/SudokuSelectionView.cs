using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using _Assets.Scripts.Services.UIs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Gameplay.Sudoku.Grid.Views
{
    public class SudokuSelectionView : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        [SerializeField] private MyButton[] buttons;
        [Inject] private ConfigProvider _configProvider;
        private ISudokuCellView _sudokuCellView;
        [Inject] private SudokuGridController _sudokuGridController;

        private void Awake()
        {
            for (var i = 1; i < images.Length; i++)
            {
                images[i].sprite = _configProvider.SudokuSkin.Sprites[i];
            }
        }

        private void OnLeftClick(int index)
        {
            if (index == 0)
            {
                _sudokuGridController.Reset(_sudokuCellView);
            }
            else
            {
                _sudokuGridController.SetNumber(_sudokuCellView, index);
            }
        }

        private void OnRightClick(int index)
        {
            if (index == 0)
            {
                _sudokuGridController.Reset(_sudokuCellView);
            }
            else
            {
                _sudokuGridController.SetNumberNote(_sudokuCellView, index);
            }
        }

        public void Show(ISudokuCellView sudokuView)
        {
            transform.localScale = Vector3.zero;

            for (int i = 0; i < buttons.Length; i++)
            {
                var i1 = i;
                buttons[i].OnLeftClick += () => OnLeftClick(i1);
                buttons[i].OnRightClick += () => OnRightClick(i1);
            }

            var cellLocalPosition = sudokuView.GameObject.transform.localPosition;
            var cellPositionInGrid = sudokuView.GameObject.transform.parent.TransformPoint(cellLocalPosition);
            var newPosition = transform.parent.InverseTransformPoint(cellPositionInGrid);
            transform.localPosition = newPosition;

            _sudokuCellView = sudokuView;
            gameObject.SetActive(true);

            transform.DOScale(1f, 0.2f);
        }

        public void Hide()
        {
            transform.DOScale(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].transform.DOScale(1f, 0.1f);
            }

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Unsub();
            }
        }
    }
}
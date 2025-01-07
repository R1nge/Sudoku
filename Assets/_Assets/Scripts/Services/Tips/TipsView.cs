using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.Tips
{
    public class TipsView : MonoBehaviour
    {
        [SerializeField] private Button tipButton;
        [SerializeField] private TextMeshProUGUI tipsCountText;
        [Inject] private SudokuGridController _sudokuGridController;
        [Inject] private TipsService _tipsService;

        private void Awake()
        {
            tipButton.onClick.AddListener(UseTip);
            UpdateCount();
        }

        private void OnDestroy()
        {
            tipButton.onClick.RemoveListener(UseTip);
        }

        private void UseTip()
        {
            if (_tipsService.Show())
            {
                ShowCell();
                UpdateCount();
                Disable();
            }
        }

        private void UpdateCount()
        {
            if (_tipsService.TipsLeft > 0)
            {
                tipsCountText.text = _tipsService.TipsLeft.ToString();
            }
            else
            {
                tipsCountText.text = string.Empty;
            }
        }

        private void Disable()
        {
            if (_tipsService.TipsLeft <= 0)
            {
                tipButton.interactable = false;
            }
        }

        private void ShowCell()
        {
            var cell = _sudokuGridController.GetMostObviousCell();
            var number = _sudokuGridController.GetMostObviousCellNumber();

            if (number != null)
            {
                _sudokuGridController.SetNumberNote(cell, number.Value);
                cell.Blink(true);
            }
        }
    }
}
using System.Collections.Generic;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Controllers;
using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer.Unity;

namespace _Assets.Scripts.Gameplay.Sudoku
{
    public class SudokuPlayerInput : ITickable
    {
        private bool _enabled;
        private GraphicRaycaster _raycaster;
        private SudokuGridController _sudokuGridController;

        public void Tick()
        {
            if (!_enabled)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _sudokuGridController.Undo();
                _sudokuGridController.HideSelection();
            }

            if (Input.GetMouseButtonUp(0))
            {
                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> results = new List<RaycastResult>();
                _raycaster.Raycast(pointerEventData, results);

                bool hit = false;

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.TryGetComponent(out Button button))
                    {
                        hit = true;
                        Debug.LogWarning("Clicked on the button");
                        break;
                    }

                    if (result.gameObject.TryGetComponent(out ISudokuCellView cellView))
                    {
                        if (!_enabled)
                        {
                            break;
                        }

                        hit = true;
                        _sudokuGridController.ShowSelection(cellView);
                    }
                }

                if (!hit)
                {
                    _sudokuGridController.HideSelection();
                }
            }
        }

        public void Init(GraphicRaycaster raycaster, SudokuGridController sudokuGridController)
        {
            _raycaster = raycaster;
            _sudokuGridController = sudokuGridController;
        }

        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;
    }
}
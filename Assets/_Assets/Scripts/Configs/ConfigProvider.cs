using _Assets.Scripts.Gameplay.Sudoku.Grid.Views;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private UIConfig uiConfig;
        
        [SerializeField] private SudokuGridView sudokuGridView;

        [SerializeField] private SudokuSelectionView sudokuSelectionView;

        [SerializeField] private SudokuSkinConfig sudokuSkinConfig;
        public UIConfig UIConfig => uiConfig;
        public SudokuGridView SudokuGridView => sudokuGridView;
        public SudokuSelectionView SudokuSelectionView => sudokuSelectionView;
        public SudokuSkinConfig SudokuSkin => sudokuSkinConfig;
    }
}
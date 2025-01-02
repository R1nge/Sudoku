using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "SudokuSkinConfig", menuName = "Configs/Sudoku/Skin Config", order = 0)]
    public class SudokuSkinConfig : ScriptableObject
    {
        [SerializeField] private Sprite[] sprites;
        public Sprite[] Sprites => sprites;
    }
}
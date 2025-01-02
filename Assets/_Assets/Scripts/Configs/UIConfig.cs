using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private GameObject loadingUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject loseUI;
        public GameObject LoadingUI => loadingUI;
        public GameObject GameUI => gameUI;
        public GameObject LoseUI => loseUI;
    }
}
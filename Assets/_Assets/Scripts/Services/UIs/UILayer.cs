using System.Collections;
using UnityEngine;

namespace _Assets.Scripts.Services.UIs
{
    public class UILayer : MonoBehaviour
    {
        [SerializeField] private int layer;

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            transform.SetSiblingIndex(layer);
        }
    }
}
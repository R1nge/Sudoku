using UnityEngine;

namespace _Assets.Scripts.Misc
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        public Camera Camera => camera;
    }
}
using _Assets.Scripts.Misc;
using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Gameplay.Camera
{
    public class CameraZoomer : ITickable
    {
        private readonly CameraHandler _cameraHandler;
        private bool _enabled = true;

        private CameraZoomer(CameraHandler cameraHandler)
        {
            _cameraHandler = cameraHandler;
        }

        public void Tick()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                ZoomIn();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                ZoomOut();
            }
        }

        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        private void ZoomIn()
        {
            _cameraHandler.Camera.orthographicSize -= 0.1f;
            _cameraHandler.Camera.orthographicSize = Mathf.Clamp(_cameraHandler.Camera.orthographicSize, 0.1f, 10f);
        }

        private void ZoomOut()
        {
            _cameraHandler.Camera.orthographicSize += 0.1f;
            _cameraHandler.Camera.orthographicSize = Mathf.Clamp(_cameraHandler.Camera.orthographicSize, 0.1f, 10f);
        }
    }
}
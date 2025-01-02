using UnityEngine;
using VContainer.Unity;

namespace _Assets.Scripts.Misc
{
    public class CanvasScaler : ITickable
    {
        private readonly CameraHandler _cameraHandler;
        private readonly Vector2 _maxDistanceFromCenter = new Vector2(100, 100);
        private bool _enabled = true;
        private bool _moving;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
        private Vector3 _startScale;

        private CanvasScaler(CameraHandler cameraHandler)
        {
            _cameraHandler = cameraHandler;
        }

        public void Tick()
        {
            if (!_moving)
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

            if (Input.GetMouseButtonDown(2))
            {
                _moving = true;
                _startPosition = _cameraHandler.Camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(2))
            {
                Move();
            }

            if (Input.GetMouseButtonUp(2))
            {
                _moving = false;
                _startPosition = _cameraHandler.Camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public void Init(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
            _startScale = _rectTransform.localScale;
        }

        public void Enable() => _enabled = true;

        public void Disable() => _enabled = false;

        private void ZoomIn()
        {
            var scaleX = Mathf.Clamp(_rectTransform.localScale.x + 0.1f, _startScale.x, 2f);
            var scaleY = Mathf.Clamp(_rectTransform.localScale.y + 0.1f, _startScale.y, 2f);
            _rectTransform.localScale = new Vector2(scaleX, scaleY);
        }

        private void ZoomOut()
        {
            var scaleX = Mathf.Clamp(_rectTransform.localScale.x - 0.1f, _startScale.x, 2f);
            var scaleY = Mathf.Clamp(_rectTransform.localScale.y - 0.1f, _startScale.y, 2f);
            _rectTransform.localScale = new Vector2(scaleX, scaleY);
        }

        private void Move()
        {
            var delta = _startPosition - _cameraHandler.Camera.ScreenToWorldPoint(Input.mousePosition);
            delta.z = 0;
            _rectTransform.localPosition += delta;
            var rectTransformPosition = _rectTransform.localPosition;
            var scaledDistance = _maxDistanceFromCenter * _rectTransform.localScale;
            rectTransformPosition.x = Mathf.Clamp(rectTransformPosition.x, -scaledDistance.x, scaledDistance.x);
            rectTransformPosition.y = Mathf.Clamp(rectTransformPosition.y, -scaledDistance.y, scaledDistance.y);
            _rectTransform.localPosition = rectTransformPosition;
        }
    }
}
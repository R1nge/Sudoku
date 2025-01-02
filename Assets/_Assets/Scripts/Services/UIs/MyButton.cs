using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Assets.Scripts.Services.UIs
{
    public class MyButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick?.Invoke();
            }
        }

        public void Unsub()
        {
            var leftClickSubs = OnLeftClick?.GetInvocationList();

            if (leftClickSubs != null)
            {
                for (int i = 0; i < leftClickSubs.Length; i++)
                {
                    if (leftClickSubs[i] != null)
                    {
                        OnLeftClick -= (Action)leftClickSubs[i];
                    }
                }
            }

            var rightClickSubs = OnRightClick?.GetInvocationList();

            if (rightClickSubs != null)
            {
                for (int i = 0; i < rightClickSubs.Length; i++)
                {
                    if (rightClickSubs[i] != null)
                    {
                        OnRightClick -= (Action)rightClickSubs[i];
                    }
                }
            }
        }

        public event Action OnLeftClick;
        public event Action OnRightClick;
    }
}
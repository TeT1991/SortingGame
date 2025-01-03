using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NuclearDecline.UI
{
    public class UIButton : MonoBehaviour, IPointerDownHandler
    {
        public Action OnClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}

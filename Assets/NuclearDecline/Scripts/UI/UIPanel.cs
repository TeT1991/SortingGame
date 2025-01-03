using UnityEngine;

namespace NuclearDecline.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] protected UIButton CloseButton;
        [SerializeField] protected UIButton OpenButton;

        protected virtual void OnEnable()
        {
            Subscribe();
        }

        protected virtual void OnDisable()
        {
            UnSubscribe();
        }

        protected virtual void Subscribe()
        {
            if (CloseButton != null)
            {
                CloseButton.OnClick += Hide;
            }

            if (OpenButton != null)
            {
                OpenButton.OnClick += Show;
            }
        }

        protected virtual void UnSubscribe()
        {
            if (CloseButton != null)
            {
                CloseButton.OnClick -= Hide;
            }

            if (OpenButton != null)
            {
                OpenButton.OnClick -= Show;
            }
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}


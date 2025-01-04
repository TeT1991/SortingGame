using UnityEngine;

namespace NuclearDecline.UI
{
    public class MainMenuPanel : UIPanel
    {
        [SerializeField] private UIButton[] _buttons;

        protected override void Subscribe()
        {
            foreach (var button in _buttons)
            {
                button.OnClick += Hide;
            }
        }

        protected override void UnSubscribe()
        {
            foreach (var button in _buttons)
            {
                button.OnClick -= Hide;
            }
        }
    }

}


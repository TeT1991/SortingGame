using UnityEngine;

namespace NuclearDecline.UI
{
    public class EndLevelPanel:UIPanel
    {
        [SerializeField] private UIButton _nextLevelButton;

        public UIButton NextLevelButton => _nextLevelButton;

    }
}


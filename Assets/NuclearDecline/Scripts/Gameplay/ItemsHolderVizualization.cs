using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.Gameplay
{
    public class ItemsHolderVizualization : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _glow;

        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;

        public void ShowGlow(HolderStatus status)
        {
            switch (status)
            {
                case HolderStatus.NotSelected:
                    SetGlow(false);
                    break;

                case HolderStatus.Selected:
                case HolderStatus.MouseOn:
                    SetGlow(true, _selectedColor);
                    break;

                case HolderStatus.Correct:
                    SetGlow(true, _correctColor);
                    break;

                case HolderStatus.Wrong:
                    SetGlow(true, _wrongColor);
                    break;
            }
        }

        private void SetGlow(bool isActive, Color color = default)
        {
            _glow.gameObject.SetActive(isActive);
            _glow.color = color;
        }
    }
}


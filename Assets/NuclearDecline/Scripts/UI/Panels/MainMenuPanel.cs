using NuclearDecline.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

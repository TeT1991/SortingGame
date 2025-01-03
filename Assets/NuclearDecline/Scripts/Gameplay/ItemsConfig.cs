using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.Gameplay
{
    [CreateAssetMenu(fileName = "Items Config")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private Color[] _colors = new Color[Enum.GetNames(typeof(ItemTypes)).Length];

        public Color GetColor(int index)
        {
            return _colors[index];
        }
    }
}
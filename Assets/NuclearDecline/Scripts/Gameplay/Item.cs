using UnityEngine;

namespace NuclearDecline.Gameplay
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private ItemTypes _type;

        public ItemTypes Type => _type;

        public void SetType(ItemTypes type)
        {
            _type = type;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}


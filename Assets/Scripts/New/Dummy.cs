using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Dummy : MonoBehaviour, BeingWatched
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void Watched()
        {
            Color Color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(Color.r, Color.g, Color.b, 0.3f);
        }

        public void Out()
        {
            Color Color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(Color.r, Color.g, Color.b, 0f);
        }
    }
}
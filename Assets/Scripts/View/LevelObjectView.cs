using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer _spriteRenderer;
        public Rigidbody2D _rb;
        public Collider2D _collider;

        //public Transform Transform => _transform;

        //public SpriteRenderer SpriteRenderer => _spriteRenderer; 

        private void Awake()
        {
            _transform = transform;
            if(!TryGetComponent<SpriteRenderer>(out _spriteRenderer)) Debug.Log("Not Component SpriteRenderer");
            if(!TryGetComponent<Rigidbody2D>(out _rb)) Debug.Log("Not Component Rigidbody2D"); ;
            if(!TryGetComponent<Collider2D>(out _collider)) Debug.Log("Not Component Collider2D"); ;
        }
    }
}
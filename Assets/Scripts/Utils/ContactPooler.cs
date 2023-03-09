using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ContactPooler
    {
        private Collider2D _collider;
        private ContactPoint2D[] _contacts = new ContactPoint2D[5];
        private int _contactCount = 0;
        private float _trashold = 0.2f;

        public bool IsGrounded { get; private set; }
        public bool LeftContact { get; private set; }
        public bool RightContact { get; private set; }

        public ContactPooler(Collider2D collider) 
        { 
            _collider = collider;
        }

        public void Update()
        {
            IsGrounded = false;
            LeftContact = false;
            RightContact = false;

            _contactCount = _collider.GetContacts(_contacts);

            for(int i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > _trashold) IsGrounded = true;
                if (_contacts[i].normal.x > _trashold) LeftContact = true;
                if (_contacts[i].normal.x < _trashold) RightContact = true;
            }
        }
    }
}
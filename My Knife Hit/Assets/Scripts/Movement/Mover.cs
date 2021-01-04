using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.Movement
{
    public class Mover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private Vector2 _velocity = new Vector2();

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void SetVelocity(Vector2 velocity)
        {
            this._velocity = velocity;
            _rigidbody2D.velocity = _velocity;
        }

        public void SwitchRigidbodyType(RigidbodyType2D rigidbodyType2D)
        {
            _rigidbody2D.bodyType = rigidbodyType2D;
        }
    }
}

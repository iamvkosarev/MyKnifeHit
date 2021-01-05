using UnityEngine;
using System.Collections;
using System;

namespace KnifeHit.Movement
{
    public class Rotator : MonoBehaviour
    {
        // Degrees per second
        private float _speed = 0f;
        private float _rotateSign = 1f;

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetRotationSide(bool clockwiseRotation = true)
        {
            _rotateSign = clockwiseRotation ? 1 : -1;
        }

        public void SetRotationSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.forward * _speed * _rotateSign * Time.fixedDeltaTime);
        }
    }
}
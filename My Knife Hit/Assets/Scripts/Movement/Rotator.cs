using UnityEngine;
using System.Collections;
using System;

namespace KnifeHit.Movement
{
    public class Rotator : MonoBehaviour
    {
        private float _timeOnStartRotate = 0f;
        // Degrees per second
        private float _speed = 0f;
        private float _rotateSign = 1f;
        private float pastTime = 0f;


        public float GetSpeed()
        {
            return _speed;
        }

        public void SetTimeOnStartRotation(float time = 0f)
        {
            this._timeOnStartRotate = time;
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
            if (pastTime < _timeOnStartRotate)
            {
                pastTime += Time.deltaTime;
                Rotate(_speed * (1- Mathf.Pow((_timeOnStartRotate - pastTime )/ _timeOnStartRotate, 2)));
            }
            else
            {
                Rotate(_speed);
                pastTime = _timeOnStartRotate;
            }
        }

        private void Rotate(float speed)
        {
            transform.Rotate(Vector3.forward * speed * _rotateSign * Time.fixedDeltaTime);
        }
    }
}
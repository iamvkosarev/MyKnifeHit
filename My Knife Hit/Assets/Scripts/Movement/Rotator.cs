using UnityEngine;
using System.Collections;
using System;

namespace KnifeHit.Movement
{

    public enum TypeOfRotation
    {
        PeriodRotation,
        Normal
    }
    public class Rotator : MonoBehaviour
    {
        private float _timeOnStartRotate = 0f;
        // Degrees per second
        private float _speed = 0f;
        private float _rotateSign = 1f;
        private float _pastTime = 0f;
        private float _periodOfRotation = 0f;
        private TypeOfRotation _typeOfRotation = TypeOfRotation.Normal;

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetTypeOfRotation(TypeOfRotation typeOfRotation = TypeOfRotation.Normal)
        {
            this._typeOfRotation = typeOfRotation;
        }

        public void SetPeriodOfRotation(float periodOfRotation)
        {
            this._periodOfRotation = periodOfRotation;
        }

        public void SetTimeOnStartRotation(float time = 0f)
        {
            this._timeOnStartRotate = time;
            if (_typeOfRotation == TypeOfRotation.PeriodRotation)
            {
                _pastTime = _timeOnStartRotate;
            }
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
            if (NormalRotation()) { return; }
            if (PeriodRotation()) { return; }
            
        }

        private bool PeriodRotation()
        {
            if (_typeOfRotation != TypeOfRotation.PeriodRotation) { return false; }
            if (_pastTime < _periodOfRotation)
            {
                _pastTime += Time.deltaTime;
                Rotate(_speed *  Mathf.Sin(Mathf.PI * 2f * _pastTime/_periodOfRotation));
            }
            else
            {
                _pastTime -= _periodOfRotation;
            }
            return true;
        }

        private bool NormalRotation()
        {
            if (_typeOfRotation != TypeOfRotation.Normal) { return false; }
            if (_pastTime < _timeOnStartRotate)
            {
                _pastTime += Time.deltaTime;
                Rotate(_speed * (1 - Mathf.Pow((_timeOnStartRotate - _pastTime) / _timeOnStartRotate, 2)));
            }
            else
            {
                Rotate(_speed);
                _pastTime = _timeOnStartRotate;
            }
            return true;
        }

        private void Rotate(float speed)
        {
            transform.Rotate(Vector3.forward * speed * _rotateSign * Time.fixedDeltaTime);
        }
    }
}
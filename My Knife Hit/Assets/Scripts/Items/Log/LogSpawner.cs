using UnityEngine;
using KnifeHit.Core;
using KnifeHit.Movement;
using System.Collections;
using System;
using KnifeHit.Items.Knife;
 
namespace KnifeHit.Items.Log
{
    public class LogSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _knifePrefab;
        [SerializeField] private GameObject _applePrefab;
        [SerializeField] private GameProperies _gameProperies;
        [SerializeField] private GameObject _logPrefab;

        private LogObj _currentLog;
        private Rotator _currentLogRotator;
        private int _numOfChildrenCurrentLog = 0;
        private float _initialAngleOfKnifeSpawn = -1f;

        private void Update()
        {
            UpdateRotationSpeedAcordingProps();
        }
        public void ExploreLog()
        {
            _currentLog.DestroyVFX();
        }
        private void UpdateRotationSpeedAcordingProps()
        {
            if (_currentLogRotator !=null && _currentLogRotator.GetSpeed() != _gameProperies.rotationSpeedOfLog)
            {
                _currentLogRotator.SetRotationSpeed(_gameProperies.rotationSpeedOfLog);
            }
        }

        public void SpawnLog()
        {
            GameObject newLog = Instantiate(_logPrefab);
            newLog.transform.position = transform.position;

            _currentLog = newLog.GetComponent<LogObj>();
            _currentLogRotator = newLog.GetComponent<Rotator>();
            SetProperiesToCurrentLog();

        }

        private void SetProperiesToCurrentLog()
        {
            _currentLogRotator.SetRotationSpeed(_gameProperies.rotationSpeedOfLog);
            _currentLogRotator.SetRotationSide(UnityEngine.Random.Range(0,2) == 1);
            _currentLogRotator.SetTimeOnStartRotation(
                UnityEngine.Random.Range(_gameProperies.minTimeOnStartRotation, _gameProperies.maxTimeOnStartRotation));
        }

        public void DestroyLog()
        {
            if (_currentLog)
            {
                Destroy(_currentLog.gameObject);
            }
            _numOfChildrenCurrentLog = 0;
        }
        public void SpawnAppleOnLog()
        {
            if(_initialAngleOfKnifeSpawn == -1)
            {
                _initialAngleOfKnifeSpawn = UnityEngine.Random.Range(0f, 360f);
            }
            else
            {
                _initialAngleOfKnifeSpawn += 60f;
            }

            float knifeInitialAngle = _applePrefab.transform.eulerAngles.z;
            float colliderRadius = _logPrefab.GetComponent<CircleCollider2D>().radius * 1.8f;
            GameObject appleObject = Instantiate(_applePrefab);
            appleObject.transform.parent = _currentLog.transform;
            appleObject.transform.localPosition = new Vector2(
                colliderRadius * Mathf.Cos(_initialAngleOfKnifeSpawn * Mathf.PI / 180f),
                colliderRadius * Mathf.Sin(_initialAngleOfKnifeSpawn * Mathf.PI / 180f));
            appleObject.transform.rotation = Quaternion.Euler(Vector3.forward * (_initialAngleOfKnifeSpawn + 90 + knifeInitialAngle));

            _numOfChildrenCurrentLog += 1;
            _currentLog.SetStartNumOfChildren(_numOfChildrenCurrentLog);
        }
        public void SpawnKnifeOnLog(int num)
        {
            _initialAngleOfKnifeSpawn = UnityEngine.Random.Range(0f, 360f);
            float angleOfStep = 360f / num;
            float knifeInitialAngle = _knifePrefab.transform.eulerAngles.z;
            float colliderRadius = _logPrefab.GetComponent<CircleCollider2D>().radius * 1.55f;
            for (int i = 0; i < num; i++)
            {
                GameObject knifeObject = Instantiate(_knifePrefab);

                float angleOfRotation = (_initialAngleOfKnifeSpawn + angleOfStep * i) ;
                knifeObject.transform.parent = _currentLog.transform;
                knifeObject.transform.localPosition = new Vector2(
                    colliderRadius * Mathf.Cos(angleOfRotation * Mathf.PI / 180f),
                    colliderRadius * Mathf.Sin(angleOfRotation * Mathf.PI / 180f));
                knifeObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angleOfRotation + 90 + knifeInitialAngle));
                
            }

            _numOfChildrenCurrentLog += num;
            _currentLog.SetStartNumOfChildren(_numOfChildrenCurrentLog);
        }

    }
}
using UnityEngine;
using KnifeHit.Core;
using KnifeHit.Movement;
using System.Collections;
using System;
using KnifeHit.Knife;
 
namespace KnifeHit.Log
{
    public class LogSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _knifePrefab;
        [SerializeField] private GameProperies _gameProperies;
        [SerializeField] private GameObject _logPrefab;

        private LogObj _currentLog;
        private Rotator _currentLogRotator;


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
            _currentLogRotator.SetRotationSide();
            _currentLogRotator.SetTimeOnStartRotation(
                UnityEngine.Random.Range(_gameProperies.minTimeOnStartRotation, _gameProperies.maxTimeOnStartRotation));
        }

        public void DestroyLog()
        {
            if (_currentLog)
            {
                Destroy(_currentLog.gameObject);
            }
        }
        public void SpawnKnifeOnLog(int num)
        {
            float initialAngle = UnityEngine.Random.Range(0f, 360f);
            float angleOfStep = 360f / num;
            float knifeInitialAngle = _knifePrefab.transform.eulerAngles.z;
            float colliderRadius = _logPrefab.GetComponent<CircleCollider2D>().radius * 1.55f;
            for (int i = 0; i < num; i++)
            {
                GameObject startKnifeObject = Instantiate(_knifePrefab);

                float angleOfRotation = (initialAngle + angleOfStep * i) ;
                startKnifeObject.transform.parent = _currentLog.transform;
                startKnifeObject.transform.localPosition = new Vector2(
                    colliderRadius * Mathf.Cos(angleOfRotation * Mathf.PI / 180f),
                    colliderRadius * Mathf.Sin(angleOfRotation * Mathf.PI / 180f));
                startKnifeObject.transform.rotation = Quaternion.Euler(Vector3.forward * (angleOfRotation + 90 + knifeInitialAngle));
                
            }

            _currentLog.SetStartNumOfChildren(num);
        }
    }
}
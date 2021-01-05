using UnityEngine;
using KnifeHit.Core;
using KnifeHit.Movement;
using System.Collections;
using System;

namespace KnifeHit.Log
{
    public class LogSpawner : MonoBehaviour
    {
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
        }

        public void DestroyLog()
        {
            if (_currentLog)
            {
                Destroy(_currentLog.gameObject);
            }
        }
    }
}
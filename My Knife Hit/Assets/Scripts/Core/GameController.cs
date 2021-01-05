﻿using UnityEngine;
using System;
using System.Collections;
using KnifeHit.Knife;
using KnifeHit.Log;

namespace KnifeHit.Core
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameProperies _gameProperies;
        [SerializeField] private GameObject _logSpawnerPrefab;
        [SerializeField] private GameObject _knifeSpawnerPrefab;
        public static GameController instance = null;

        private LogSpawner _logSpawner;
        private KnifeSpawner _knifeSpawner;
        private int _numOfKnivesToSpawn;
        private int _numOfHitLog;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }

            InitializeObjects();
        }
        private void InitializeObjects()
        {
            InitializeKnife();
            InitializeLog();

        }

        private void InitializeLog()
        {
            _logSpawner = _logSpawnerPrefab.GetComponent<LogSpawner>();
            _logSpawner.SpawnLog();
        }

        private void InitializeKnife()
        {
            _knifeSpawner = _knifeSpawnerPrefab.GetComponent<KnifeSpawner>();
            _numOfKnivesToSpawn = UnityEngine.Random.Range(_gameProperies.minNumOfKnivesThrow,
                _gameProperies.maxNumOfKnivesThrow +1);
            _knifeSpawner.SetNumToSpawn(_numOfKnivesToSpawn);
            _knifeSpawner.SpawnNewKnife();
        }

        public void RestartGame()
        {
            StartCoroutine(LoadingRestart());
        }

        public void AddHitPoint()
        {
            _numOfHitLog++;
            if (_numOfHitLog == _numOfKnivesToSpawn)
            {
                _logSpawner.ExploreLog();
                RestartGame();
            }
        }
        IEnumerator LoadingRestart()
        {
            yield return new WaitForSeconds(_gameProperies.restartGameDelay);
            //LoadLoseCanvas();
            _numOfHitLog = 0;
            _logSpawner.DestroyLog();
            _knifeSpawner.DestroyCurrentKnife();
            Debug.Log("Инициализировать объекты заново");
            InitializeObjects();
        }
    }
}
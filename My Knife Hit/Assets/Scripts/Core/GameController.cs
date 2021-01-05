using UnityEngine;
using System;
using System.Collections;
using KnifeHit.Items.Knife;
using KnifeHit.Items.Log;

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
        private int _applePoints = 0;
        private int _numOfPassedLevels = 0;

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
            UploadData();
        }

        private void UploadData()
        {
            ProgressData progressData = SaveSystem.LoadProgress();
            _applePoints = progressData.resultingNumOfApples;
            _numOfPassedLevels = progressData.recordLevelsPassed;
            UIController.instance.RefreshData(_applePoints, _numOfPassedLevels, _numOfKnivesToSpawn, _numOfHitLog);
        }
        private void RefreshProgressData()
        {
            UIController.instance.RefreshData(_applePoints, _numOfPassedLevels, _numOfKnivesToSpawn, _numOfHitLog);
            ProgressData progressData = SaveSystem.LoadProgress();
            progressData.resultingNumOfApples = _applePoints;
            progressData.recordLevelsPassed = Math.Max(_numOfPassedLevels, SaveSystem.LoadProgress().recordLevelsPassed);
            SaveSystem.SaveProgress(progressData);
            UIController.instance.RefreshData(_applePoints, _numOfPassedLevels, _numOfKnivesToSpawn, _numOfHitLog);
        }

        private void InitializeObjects()
        {
            _numOfKnivesToSpawn = UnityEngine.Random.Range(_gameProperies.minNumOfKnivesThrow,
                   _gameProperies.maxNumOfKnivesThrow + 1);
            _logSpawner = _logSpawnerPrefab.GetComponent<LogSpawner>();
            _knifeSpawner = _knifeSpawnerPrefab.GetComponent<KnifeSpawner>();

        }
        public void FirstGameStart()
        {
            UIController.instance.OpenCanvas(TypeOfUICanvas.Game);
            _numOfHitLog = 0;
            _numOfPassedLevels = 0;
            RefreshProgressData();
            StartSpawningLog();
            StartSpawningKnives();
        }
        public void StartGame()
        {
            UIController.instance.OpenCanvas(TypeOfUICanvas.Game);
            _numOfHitLog = 0;
            _numOfKnivesToSpawn = UnityEngine.Random.Range(_gameProperies.minNumOfKnivesThrow,
                _gameProperies.maxNumOfKnivesThrow + 1);
            RefreshProgressData();
            StartSpawningLog();
            StartSpawningKnives();
        }
        private void StartSpawningLog()
        {
            
            _logSpawner.SpawnLog();
            _logSpawner.SpawnKnifeOnLog(UnityEngine.Random.Range(_gameProperies.minNumOfStartKnives,
                _gameProperies.maxNumOfStartKnives+1));
            if (UnityEngine.Random.Range(0f, 1f) < _gameProperies.chanceOfAppleAppearing)
            {
                _logSpawner.SpawnAppleOnLog();
            }
        }

        private void StartSpawningKnives()
        {
            _knifeSpawner.SetNumToSpawn(_numOfKnivesToSpawn);
            _knifeSpawner.SpawnNewKnife();
        }

        public void LoadLoseCanvas()
        {
            StartCoroutine(LoadingRestart());
        }

        public void RestartGame()
        {
            RefreshProgressData();
            _numOfPassedLevels = 0;
            UpdateLevel();
        }

        public void NextLevel()
        {
            _numOfPassedLevels++;
            RefreshProgressData();
            StartCoroutine(LoadingUpdateLevel());
        }

        public void AddHitPoint()
        {
            _numOfHitLog++;
            if (_numOfHitLog == _numOfKnivesToSpawn)
            {
                _logSpawner.ExploreLog();

                NextLevel();
            }
            UIController.instance.RefreshData(_applePoints, _numOfPassedLevels, _numOfKnivesToSpawn, _numOfHitLog);
        }

        public void AddApplePoint()
        {
            _applePoints++;
            RefreshProgressData();
            UIController.instance.RefreshData(_applePoints, _numOfPassedLevels, _numOfKnivesToSpawn, _numOfHitLog);
        }


        
        IEnumerator LoadingUpdateLevel()
        {
            yield return new WaitForSeconds(_gameProperies.restartGameDelay);
            UpdateLevel();
        }
        IEnumerator LoadingRestart()
        {
            yield return new WaitForSeconds(_gameProperies.restartGameDelay);

            _numOfPassedLevels = SaveSystem.LoadProgress().recordLevelsPassed;
            RefreshProgressData();
            UIController.instance.OpenCanvas(TypeOfUICanvas.Lose);
        }

        private void UpdateLevel()
        {
            _logSpawner.DestroyLog();
            _knifeSpawner.DestroyCurrentKnife();
            StartGame();
        }

    }
}
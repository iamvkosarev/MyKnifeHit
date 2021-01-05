using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using KnifeHit.UI;
using System;

namespace KnifeHit.Core
{
    public enum TypeOfUICanvas
    {
        MainMenu,
        Lose,
        Game
    }
    public class UIController : MonoBehaviour
    {

    
        [SerializeField] private GameObject _mainMenuCanvasPrefab;
        [SerializeField] private GameObject _loseCanvasPrefab;
        [SerializeField] private GameObject _gameCanvasPrefab;
        [SerializeField] private GameObject _knifeUIImagePrefab;


        [SerializeField] private RectTransform _rectFormForKnives;
        [SerializeField] private TextMeshProUGUI[] applesTMPros;
        [SerializeField] private TextMeshProUGUI[] levelsRecordTMPros;
        public static UIController instance = null;

        private int _numOfKnivesGenerally;
        private int _numOfTrownsKnives;
        private int _numOfDestroyedKnifeImages;
        private int _applesNum;
        private int _levelsRecord;
        private RectTransform _rectFormOfKnives;
        private LoseCanvas _loseCanas;


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

            _loseCanas = _loseCanvasPrefab.GetComponent<LoseCanvas>();
            _rectFormOfKnives = _knifeUIImagePrefab.GetComponent<RectTransform>();
        }

        public void RefreshData(int applesNum, int levelsRecord, int numOfKnivesGenerally, int numOfTrownsKnives)
        {
            this._applesNum = applesNum;
            this._numOfKnivesGenerally = numOfKnivesGenerally;
            this._numOfTrownsKnives = numOfTrownsKnives;
            this._levelsRecord = levelsRecord;
            RefreshDataIForms();
        }

        private void RefreshDataIForms()
        {
            for (int i = 0; i < applesTMPros.Length; i++)
            {
                applesTMPros[i].text = _applesNum.ToString();
            }
            for (int i = 0; i < levelsRecordTMPros.Length; i++)
            {
                levelsRecordTMPros[i].text = _levelsRecord.ToString();
            }
            if(_numOfTrownsKnives != _numOfDestroyedKnifeImages)
            {
                DestroyTrownKnifes();
            }
            if(_rectFormForKnives.transform.childCount != _numOfKnivesGenerally)
            {
                RefreshKnivesForm();
            }
        }

        private void DestroyTrownKnifes()
        {
            int childCount = _rectFormForKnives.transform.childCount;
            for (int i = 0; i < _numOfTrownsKnives - _numOfDestroyedKnifeImages; i++)
            {
                _rectFormForKnives.transform.GetChild(childCount - 1 - i).GetComponent<KnifeImageForm>().StartDestroy();
            }
            _numOfDestroyedKnifeImages = _numOfTrownsKnives;
        }

        private void RefreshKnivesForm()
        {
            foreach (Transform child in _rectFormForKnives.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            float startPosForKnifeForm = _rectFormForKnives.rect.height / 2f - _rectFormOfKnives.rect.height / 2f;
            float stepForNextXAxisPos = _rectFormOfKnives.rect.height;
            for (int i = 0; i < _numOfKnivesGenerally - _numOfTrownsKnives; i++)
            {
                GameObject newKnifeImage = Instantiate(_knifeUIImagePrefab);
                newKnifeImage.transform.parent = _rectFormForKnives.transform;
                RectTransform rect = newKnifeImage.GetComponent<RectTransform>();
                rect.localScale = new Vector3(1, 1, 1);
                rect.transform.localPosition = new Vector2(_rectFormOfKnives.rect.width/2f, - startPosForKnifeForm + stepForNextXAxisPos * i);
            }
        }

        private void Start()
        {
            OpenCanvas(TypeOfUICanvas.MainMenu);
        }
        private void SwitchOffAllCanvases()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        public void OpenCanvas(TypeOfUICanvas typeOfUICanvas)
        {
            SwitchOffAllCanvases();
            if (typeOfUICanvas == TypeOfUICanvas.Game)
            {
                _gameCanvasPrefab.SetActive(true);
            }
            else if(typeOfUICanvas == TypeOfUICanvas.Lose)
            {
                _loseCanvasPrefab.SetActive(true);
            }
            else if(typeOfUICanvas == TypeOfUICanvas.MainMenu)
            {
                _mainMenuCanvasPrefab.SetActive(true);
            }
        }
    }
}

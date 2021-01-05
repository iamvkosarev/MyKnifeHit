using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

        [SerializeField] private TextMeshProUGUI[] applesTMPros;
        [SerializeField] private TextMeshProUGUI[] levelsRecordTMPros;
        public static UIController instance = null;

        private int _applesNum;
        private int _levelsRecord;
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

            this._loseCanas = _loseCanvasPrefab.GetComponent<LoseCanvas>();
        }

        public void RefreshData(int applesNum, int levelsRecord)
        {
            this._applesNum = applesNum;
            this._levelsRecord = levelsRecord;
            RefreshDataInTMPro();
        }

        private void RefreshDataInTMPro()
        {
            for (int i = 0; i < applesTMPros.Length; i++)
            {
                applesTMPros[i].text = _applesNum.ToString();
            }
            for (int i = 0; i < levelsRecordTMPros.Length; i++)
            {
                levelsRecordTMPros[i].text = _levelsRecord.ToString();
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

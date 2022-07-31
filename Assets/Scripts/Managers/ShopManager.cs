using System;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using UnityEngine.UI;
using Data.UnityObject;
using Enums;
using TMPro;
using System.Collections;
using Data.ValueObject;

namespace UIShop
{
    public class ShopManager : MonoBehaviour
    {   
        [SerializeField]
        TextMeshProUGUI textMeshProUGUI;

        private string _name;
        private int _firstCharID = 1;
        private int _charPrice;
        private Button _charButton;
        private GameObject _lock;
        private float _currentMoney;
        //[SerializeField]
        public ShopData shopData;
       
        private void Awake()
        {
            _charButton = GetComponent<Button>();
            _lock = GetComponent<GameObject>();
        }

        private void GetCharData(int i)
        {
            _name = shopData.CharData[i].CharName;
            _charPrice = shopData.CharData[i].CharPrice;
            _charButton = shopData.CharData[i].CharButton;
            _lock = shopData.CharData[i].Lock;
            
        }

        private void OnEnable()
        {
            SubscribeEvents();

        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OpenShopPanel;
            SaveLoadSignals.Instance.onGetTotalWealth += OnGetTotalWealth;
        }


        private void UnSubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OpenShopPanel;
            SaveLoadSignals.Instance.onGetTotalWealth -= OnGetTotalWealth;
            
        }

        private void OnGetTotalWealth(float value)
        {
            _currentMoney = value;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void OpenShopPanel(UIPanels panels)
        { 
            panels = UIPanels.StorePanel;
            if ((int)panels==0)
            {
                GetCharData(_firstCharID);
                OnGetMoney();
            }
        }
        private void OnGetMoney()
        {

            if (_charPrice < _currentMoney )
            {

                _lock.GetComponent<Button>().enabled = true;
            }
        }

        public void UnlockCharcater()
        {
            if (_charPrice < _currentMoney)
            {

                _lock.SetActive(false);
                _charButton.enabled = true;
                _firstCharID++;

            }

            _currentMoney = _currentMoney - _charPrice;
            ScoreSignals.Instance.onScoreChange?.Invoke(ScoreTypes.TotalGameScore, _currentMoney);

        }

        public void SelectChar(GameObject buttonName)
        {
            ShopSignal.Instance?.onChangeChar(buttonName.name);
            
        }

    }
}

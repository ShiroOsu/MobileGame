using System;
using System.Collections.Generic;
using Code.Counter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<ButtonInfo> m_InfoList;
        private CounterManager m_Counter;
        
        [Serializable]
        public class ButtonInfo
        {
            public Button button;
            public int baseCost;
            public float incomePerSecond;
            [HideInInspector] public int numberOfBought;
            public TMP_Text buttonTextCost;
            public TMP_Text buttonIncomeGain;
            [HideInInspector] public bool canBuy = false;
        }
        
        private void Awake()
        {
            m_Counter = CounterManager.Instance;
            
            for (int i = 0; i < m_InfoList.Count; i++)
            {
                var y = i;
                m_InfoList[i].button.onClick.AddListener(() => Buy(y));
            }

            foreach (var b in m_InfoList)
            {
                UpdateText(b);
            }
        }

        private void Update()
        {
            UpdateButtonCanBuy();
        }

        private void Buy(int index)
        {
            var buttonInfo = m_InfoList[index];

            if (buttonInfo.baseCost > m_Counter.CurrentMoney)
                return;
            
            buttonInfo.numberOfBought += 1;
            m_Counter.CurrentMoney -= buttonInfo.baseCost;
            m_Counter.AddIncome(buttonInfo.incomePerSecond);
            buttonInfo.baseCost += Mathf.RoundToInt(buttonInfo.baseCost * 0.1f);
            UpdateText(buttonInfo);
        }

        private void UpdateButtonCanBuy()
        {
            foreach (var infoButton in m_InfoList)
            {
                infoButton.canBuy = m_Counter.CurrentMoney >= infoButton.baseCost;
                infoButton.button.interactable = infoButton.canBuy;
            }
        }

        private void UpdateText(ButtonInfo buttonInfo)
        {
            buttonInfo.buttonTextCost.SetText("Buy: " + buttonInfo.baseCost + "$" + "\n Owned: " + buttonInfo.numberOfBought);
            buttonInfo.buttonIncomeGain.SetText("Income: " + buttonInfo.incomePerSecond);
        }
    }
}
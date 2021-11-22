using System;
using System.Collections.Generic;
using Code.Counter;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<ButtonStruct> m_StructList;
        private float m_CurrentMoney;
        private CounterManager m_Counter;
        
        [Serializable]
        public struct ButtonStruct
        {
            public Button button;
            public int baseCost;
            public float incomePerSecond;
            public int numberOfBought;
        }
        
        private void Awake()
        {
            m_Counter = CounterManager.Instance;
            
            for (int i = 0; i < m_StructList.Count; i++)
            {
                var y = i;
                m_StructList[i].button.onClick.AddListener(() => Buy(y));
            }
        }

        private void FixedUpdate()
        {
            m_CurrentMoney = m_Counter.CurrentMoney;
            UpdateButtonCanBuy();
        }

        private void Start()
        {
            m_CurrentMoney = m_Counter.CurrentMoney;
            UpdateButtonCanBuy();
        }

        private void Buy(int index)
        {
            var buttonStruct = m_StructList[index];
            buttonStruct.numberOfBought++;
            m_Counter.AddIncome(buttonStruct.incomePerSecond);
            buttonStruct.baseCost += Mathf.RoundToInt(buttonStruct.baseCost * (buttonStruct.numberOfBought + 1) * 0.25f);
        }

        private void UpdateButtonCanBuy()
        {
            foreach (var buttonStruct in m_StructList)
            {
                buttonStruct.button.enabled = buttonStruct.baseCost > m_CurrentMoney;
            }
        }
    }
}
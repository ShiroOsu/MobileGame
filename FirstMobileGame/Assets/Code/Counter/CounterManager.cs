using Code.Tools;
using TMPro;
using UnityEngine;

namespace Code.Counter
{
    public class CounterManager : Singleton<CounterManager>
    {
        [SerializeField] private TMP_Text m_CounterText;
        [SerializeField] private TMP_Text m_IncomePerSecond;
        public float CurrentMoney { get; private set; }
        public float CurrentIncomePerSecond { get; private set; }
        private const float m_PerSecond = 1.0f;
        private float m_CurrentTime;

        private void Awake()
        {
            // Temp, save game
            CurrentMoney = 0;
            CurrentIncomePerSecond = 0.0f;
        }

        private void Start()
        {
            UpdateCounters();
        }

        public void AddMoney(float amount)
        {
            CurrentMoney += amount;
        }

        public void AddIncome(float amount)
        {
            CurrentIncomePerSecond += amount;
        }

        public void AddIncomeToMoney()
        {
            CurrentMoney += CurrentIncomePerSecond;
        }

        public void UpdateCounters()
        {
            m_CounterText.SetText(CurrentMoney.ToString("F1") + "$");
            m_IncomePerSecond.SetText(CurrentIncomePerSecond.ToString("N") + " $/s");
        }
    }
}

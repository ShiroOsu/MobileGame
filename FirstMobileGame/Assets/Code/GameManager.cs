using System.Collections.Generic;
using Code.Counter;
using Code.Prefab;
using Code.Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_FallingPrefab;
        [SerializeField] private CounterManager m_Counter;
        [SerializeField] private Transform m_SpawnHeight;
        private List<GameObject> m_FallingList;
        private uint m_MaxObjectsFalling;
        private uint m_NumberOfObjectsFalling;
        private ObjectPool m_FallingStuff;

        // Add income
        private const float m_PerSecond = 1.0f;
        private float m_CurrentTime;


        // Add falling object
        private const float m_PerTen = 10.0f;
        private float m_PerTenCurrent;

        private void Awake()
        {
            m_FallingStuff = new ObjectPool(100, m_FallingPrefab, new GameObject("FallingStuff").transform);
            m_FallingList = new();
        }

        private void Update()
        {
            m_Counter.UpdateCounters();
            
            m_CurrentTime += Time.deltaTime;
            if (m_CurrentTime > m_PerSecond)
            {
                SpawnFallingObject();
                m_Counter.AddIncomeToMoney();
                m_CurrentTime = 0f;
            }
        }

        private void FixedUpdate()
        {
            UpdateAmountOfFallingObjects();
        }

        private void UpdateAmountOfFallingObjects()
        {
            m_PerTenCurrent += Time.fixedDeltaTime;
            
            if (m_PerTenCurrent > m_PerTen)
            {
                m_MaxObjectsFalling = (uint) Mathf.RoundToInt(m_Counter.CurrentIncomePerSecond);
                if (m_MaxObjectsFalling > 100)
                {
                    m_MaxObjectsFalling = 100;
                }
                m_PerTenCurrent = 0f;
            }
        }

        private void SpawnFallingObject()
        {
            ReleaseList();
            
            if (m_NumberOfObjectsFalling > m_MaxObjectsFalling)
                return;
            
            for (int i = 0; i < m_MaxObjectsFalling; i++)
            {
                var ranX = Random.Range(-3f, 3f);
                var gravity = Random.Range(0.01f, 1f);
                var obj = m_FallingStuff.Rent(true);
                obj.TryGetComponent(out Rigidbody2D rb);
                rb.gravityScale = gravity;
                obj.transform.position = new Vector3(ranX, m_SpawnHeight.position.y, 5f);
                m_FallingList.Add(obj);
                m_NumberOfObjectsFalling++;
            }
        }

        private void ReleaseList()
        {
            if (m_FallingList.Count < 1)
                return;
            
            for (int i = 0; i < m_FallingList.Count; i++)
            {
                var obj = m_FallingList[i];
                if (obj.transform.position.y < -m_SpawnHeight.position.y && obj.gameObject.activeInHierarchy)
                {
                    obj.SetActive(false);
                    m_NumberOfObjectsFalling--;
                }
            }
        }
    }
}
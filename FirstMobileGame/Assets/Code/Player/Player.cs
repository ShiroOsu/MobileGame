using Code.Inputs;
using Code.Counter;
using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject m_PlayerSprite;
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private float m_MoneyOnPlayerClick = 1;
        [SerializeField] private CounterManager m_Counter;

        private InputManager m_InputManager;
        private Camera m_Camera;
        private Vector3 pos;
        
        private void Awake()
        {
            m_InputManager = InputManager.Instance;
            m_Camera = Camera.main;
        }

        private void OnEnable() => m_InputManager.StartTouchEvent += ClickOnPlayer;

        private void OnDisable() => m_InputManager.EndTouchEvent -= ClickOnPlayer;

        private void ClickOnPlayer(Vector2 screenPos, float time)
        {
            var screenCoords = new Vector3(screenPos.x, screenPos.y, m_Camera.nearClipPlane);
            var worldCoords = m_Camera.ScreenToWorldPoint(screenCoords);
            worldCoords.z = 0f;

            if (IsClickOnSprite(worldCoords))
            {
                m_Counter.AddMoney(m_MoneyOnPlayerClick);
            }
        }

        private bool IsClickOnSprite(Vector3 clickPos)
        {
            return m_SpriteRenderer.bounds.Contains(clickPos);
        }
    }
}

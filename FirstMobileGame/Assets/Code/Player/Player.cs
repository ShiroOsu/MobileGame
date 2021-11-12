using Code.Inputs;
using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        private InputManager m_InputManager;
        private Camera m_Camera;
        [SerializeField] private float m_LerpSpeed = 5f;
        [SerializeField] private float m_playerPadWidth = 1f; // Half
        private Vector3 pos;
        
        private void Awake()
        {
            m_InputManager = InputManager.Instance;
            m_Camera = Camera.main;
            transform.position = new Vector3(0f, -4.75f, 0f);
        }
       
        private void Update()
        {
            if (m_InputManager.Dragging)
            {
                pos = Move(m_InputManager.TouchPosition, Time.deltaTime);
            }
            else if (transform.position != pos && pos != Vector3.zero)
            {
                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * m_LerpSpeed);
            }
        }

        private Vector3 Move(Vector2 screenPosition, float time)
        {
            var screenWidth = Screen.width;
            
            Vector3 screenCoordinates = new(screenPosition.x, screenPosition.y, m_Camera.nearClipPlane);
            var worldCoordinates = m_Camera.ScreenToWorldPoint(screenCoordinates);
            worldCoordinates.z = 0f;
            worldCoordinates.y = -4.75f;

            if (worldCoordinates.x >= screenWidth - m_playerPadWidth)
            {
                worldCoordinates.x = (screenWidth - m_playerPadWidth);
            }
            
            transform.position = Vector3.Lerp(transform.position, worldCoordinates, time * m_LerpSpeed);
            return worldCoordinates;
        }
    }
}

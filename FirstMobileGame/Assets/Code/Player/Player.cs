using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerControls m_Controls;
        
        private void Awake()
        {
            m_Controls = new();
        }

        private void Start()
        {
            m_Controls.PlayerActionMap.TouchPress.started += OnTouchPress;
        }

        private void OnEnable()
        {
            m_Controls.Enable();
        }

        private void OnDisable()
        {
            m_Controls.Disable();
        }

        private void OnTouchPress(InputAction.CallbackContext context)
        {
            Debug.Log("TouchPress start");
        }
    }
}

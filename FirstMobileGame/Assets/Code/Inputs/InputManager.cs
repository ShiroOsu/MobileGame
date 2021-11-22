using System;
using Code.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Code.Inputs
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : Singleton<InputManager>
    {
        private TouchControls m_TouchControls;

        public event Action<Vector2, float> StartTouchEvent;
        public event Action<Vector2, float> EndTouchEvent; 

        private void Awake()
        {
            m_TouchControls = new();
        }

        private void Start()
        {
            m_TouchControls.Touch.TouchPress.started += OnTouchPress;
            m_TouchControls.Touch.TouchPress.canceled += OnTouchPressEnd;
        }

        private void OnEnable()
        {
            m_TouchControls.Enable();
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            m_TouchControls.Disable();
        }
        
        private void OnTouchPress(InputAction.CallbackContext context)
        {
            StartTouchEvent?.Invoke(m_TouchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
        
        private void OnTouchPressEnd(InputAction.CallbackContext context)
        {
            EndTouchEvent?.Invoke(m_TouchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
}
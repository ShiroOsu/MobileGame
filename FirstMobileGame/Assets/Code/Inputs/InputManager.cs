using Code.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Code.Inputs
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : Singleton<InputManager>, TouchControls.ITouchActions
    {
        private TouchControls m_TouchControls;
        public bool Dragging { get; private set; }
        public Vector2 TouchPosition { get; private set; }

        private void Awake()
        {
            m_TouchControls = new();
            m_TouchControls.Touch.SetCallbacks(this);
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
        
        public void OnTouchPress(InputAction.CallbackContext context)
        {
        }

        public void OnTouchPosition(InputAction.CallbackContext context)
        {
            TouchPosition = context.ReadValue<Vector2>();
        }

        public void OnTouchHold(InputAction.CallbackContext context)
        {
            Dragging = true;
        }

        public void OnTouchRelease(InputAction.CallbackContext context)
        {
            Dragging = false;
        }
    }
}

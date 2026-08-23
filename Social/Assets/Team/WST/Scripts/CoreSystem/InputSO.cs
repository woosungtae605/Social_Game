using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts.CoreSystem
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input", order = 0)]
    public class InputSO : ScriptableObject, Controls.IPlayerActions
    {
        public Vector2 MousePos { get; private set; }
        public event Action OnLeftClickAction;

        private Controls _controls;
        private void OnEnable()
        {
            if (_controls == null)
                _controls = new Controls();
            
            _controls.Player.SetCallbacks(this);
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            if (_controls == null) return;
            
            _controls.Player.Disable();
        }

        public void OnMousePos(InputAction.CallbackContext context)
        {
            MousePos = context.ReadValue<Vector2>();
        }

        public void OnMouseLeftClick(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnLeftClickAction?.Invoke();
        }
    }
}
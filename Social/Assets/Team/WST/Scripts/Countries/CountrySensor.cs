using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.UIs;
using Team.WST.Scripts.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts.Countries
{
    public class CountrySensor : MonoBehaviour
    {
        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Sensing();
            }
        }

        private void Sensing()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            ICultureShowUI cultureShowUI = null;

            if (hitCollider != null)
            {
                cultureShowUI = hitCollider.GetComponentInParent<ICultureShowUI>();
            }
            
            if (cultureShowUI == null)
                return;
            
            Bus<CultureSensorUIEvent>.RaiseEvent(new CultureSensorUIEvent(cultureShowUI));
        }
    }
}
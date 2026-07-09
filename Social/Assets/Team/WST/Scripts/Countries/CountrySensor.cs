using Team.WST.Scripts.Countries.UIs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Team.WST.Scripts.Countries
{
    public class CountrySensor : MonoBehaviour
    {
        private Camera _mainCamera = Camera.main;
        
        public void Sensing()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            ICultureShowUI cultureShowUI = null;

            if (hitCollider != null)
            {
                cultureShowUI = hitCollider.GetComponentInParent<ICultureShowUI>();
            }
            
            if (cultureShowUI == null)
            {
                // 여기서 AllCountry 보내기
            }
            else
            {
                
            }
        }
    }
}
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
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.right);

            if (hit.collider.TryGetComponent(out ICultureShowUI cultureShowUI))
            {
                //일단 여기서 나머지 처리
            }
        }
    }
}
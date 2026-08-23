using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Countries
{
    public class CountrySensor : MonoBehaviour
    {
        [SerializeField] private InputSO inputSo;
        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
            inputSo.OnLeftClickAction += Sensing;
        }

        private void OnDestroy()
        {
            inputSo.OnLeftClickAction -= Sensing;
        }

        private void Sensing()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(inputSo.MousePos);
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
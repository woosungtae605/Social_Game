using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder
{
    public class PieImagePrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image pieImage;
        
        private float _ratio;
        
        private Action<float> _onHoverEnter;
        private Action _onHoverExit;
        public void Bind(Action<float> onEnter, Action onExit)
        {
            _onHoverEnter = onEnter;
            _onHoverExit = onExit;
        }
        
        public void SetFillAmount(float fillAmount, Color color, float displayRatio)
        {
            pieImage.fillAmount = fillAmount;
            pieImage.color = color;
            _ratio = displayRatio;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _onHoverEnter?.Invoke(_ratio);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _onHoverExit?.Invoke();
        }
    }
}
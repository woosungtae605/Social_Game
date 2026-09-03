using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder
{
    public class PieImagePrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ICanvasRaycastFilter
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

        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            RectTransform rectTransform = pieImage.rectTransform;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform, screenPoint, eventCamera, out Vector2 localPoint))
                return false;
            
            Rect rect = rectTransform.rect;
            Vector2 fromCenter = localPoint - rect.center;
            float radius = Mathf.Min(rect.width, rect.height) * 0.5f;
            if (fromCenter.sqrMagnitude > radius * radius)
                return false;
            
            if (pieImage.fillAmount >= 1f)
                return true;
            if (pieImage.fillAmount <= 0f)
                return false;
            
            if (pieImage.fillMethod != Image.FillMethod.Radial360)
                return true;
            
            float angleDeg = Mathf.Atan2(fromCenter.y, fromCenter.x) * Mathf.Rad2Deg;
            float originDeg = OriginToDegrees(pieImage.fillOrigin);
            float sweptDeg = pieImage.fillClockwise
                ? Mathf.Repeat(originDeg - angleDeg, 360f)
                : Mathf.Repeat(angleDeg - originDeg, 360f);
            
            return sweptDeg / 360f <= pieImage.fillAmount;
        }
        
        
        private static float OriginToDegrees(int fillOrigin)
        {
            switch (fillOrigin)
            {
                case (int)Image.Origin360.Bottom:
                    return -90f;
                case (int)Image.Origin360.Right:
                    return 0f;
                case (int)Image.Origin360.Top:
                    return 90f;
                case (int)Image.Origin360.Left:
                    return 180f;
                default:
                    return 0f;
            }
        }
    }
}
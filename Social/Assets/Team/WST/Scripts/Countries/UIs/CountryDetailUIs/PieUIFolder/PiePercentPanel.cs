using System;
using Team.WST.Scripts.CoreSystem;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder
{
    public class PiePercentPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI percentTxt;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private Vector2 offset = new Vector2(-40f, 0f);
        
        public void Show(float ratio)
        {
            percentTxt.text = $"{ratio * 100f:0}%";
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            transform.position = inputSO.MousePos + offset;
        }
    }
}
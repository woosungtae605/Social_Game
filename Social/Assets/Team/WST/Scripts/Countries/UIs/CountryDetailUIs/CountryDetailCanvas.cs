using System;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs
{
    public class CountryDetailCanvas : MonoBehaviour
    {
        [Header("UIs")]
        [SerializeField] private Button exitBtn;

        public event Action OnExitBtnClick; 
        public void Init()
        {
            exitBtn.onClick.AddListener(HandleExitBtnClick);
        }

        private void OnDestroy()
        {
            exitBtn.onClick.RemoveListener(HandleExitBtnClick);
        }

        private void HandleExitBtnClick()
        {
            OnExitBtnClick?.Invoke();
        }

        public void Show(ICultureShowUI iCultureShowUI)
        {
            
        }
    }
}
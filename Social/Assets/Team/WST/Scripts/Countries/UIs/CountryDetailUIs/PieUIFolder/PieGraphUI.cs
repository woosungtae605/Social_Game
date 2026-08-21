using System;
using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Countries.Informations;
using Team.WST.Scripts.Countries.UIs.CountryInformationUIs;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder
{
    public class PieGraphUI : MonoBehaviour
    {
        [SerializeField] private Color ownColor;
        [SerializeField] private Color otherColor;
        
        [Header("References")]
        [SerializeField] private PiePercentPanel piePercentPanel;
        
        [Header("UIs")]
        [SerializeField] private TextMeshProUGUI totalCulturePowerTxt;
        
        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private PieImagePrefab pieImage;
        [SerializeField] private int initCount = 2;
        
        private GenericObjectPool<PieImagePrefab> _pool;

        public void Init()
        {
            _pool = new GenericObjectPool<PieImagePrefab>(pieImage, spawnTransform, initCount);
        }
        public void Show(ICultureShowUI cultureShowUI)
        {
            _pool.Clear();

            var culturePowerDict = cultureShowUI.CulturePowerDict;
            
            int totalNum = 0;

            foreach (int cultureDict in culturePowerDict.Values)
            {
                totalNum += cultureDict;
            }
            
            totalCulturePowerTxt.text = totalNum.ToString();
            
            if (totalNum <= 0)
                return;
            
            int own = 0;
            if (cultureShowUI.CulturePowerDict.TryGetValue(cultureShowUI.CountryType, out int ownPower))
                own = ownPower;
            
            int others = totalNum - own;
            float ownRatio = (float)own / totalNum;
            float otherRatio = (float)others / totalNum;
            
            if (others > 0)
                SpawnSlice(1f, otherColor, otherRatio, true);
            if (own > 0)
                SpawnSlice(ownRatio, ownColor, ownRatio, false);
        }
        
        private void SpawnSlice(float fillAmount, Color color, float displayRatio, bool setAsFirstSibling)
        {
            PieImagePrefab slice = _pool.Get();
            slice.Bind(piePercentPanel.Show, piePercentPanel.Hide);
            slice.SetFillAmount(fillAmount, color, displayRatio);
            if (setAsFirstSibling)
                slice.transform.SetAsFirstSibling();
        }
    }
}
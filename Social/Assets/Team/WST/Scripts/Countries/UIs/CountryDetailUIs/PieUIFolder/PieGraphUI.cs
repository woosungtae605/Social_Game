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
        
        [Header("UIs")]
        [SerializeField] private TextMeshProUGUI totalCulturePowerTxt;
        
        [Header("pooling")]
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private PieImagePrefab pieImage;
        [SerializeField] private int initCount = 2;
        
        private CountryManager _countryManager;
        private GenericObjectPool<PieImagePrefab> _pool;

        public void Init(CountryManager countryManager)
        {
            _countryManager = countryManager;
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
            
            if (others > 0)
            {
                var otherSlice = _pool.Get();
                otherSlice.SetFillAmount(1f, otherColor);
                otherSlice.transform.SetAsFirstSibling();
            }
            
            if (own > 0)
            {
                var ownSlice = _pool.Get();
                ownSlice.SetFillAmount(ownRatio, ownColor);
            }
        }
    }
}
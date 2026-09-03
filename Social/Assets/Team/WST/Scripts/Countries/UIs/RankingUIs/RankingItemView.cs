using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.RankingUIs
{
    public class RankingItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rankTxt;
        [SerializeField] private TextMeshProUGUI countryNameTxt;
        [SerializeField] private TextMeshProUGUI culturePowerTxt;

        public void SetRank(int rank)
        {
            rankTxt.text = rank.ToString();
        }

        public void SetCountryName(string countryName)
        {
            countryNameTxt.text = countryName;
        }

        public void SetCulturePower(int culturePower)
        {
            culturePowerTxt.text = culturePower.ToString();
        }
    }
}

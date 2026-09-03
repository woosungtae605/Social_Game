using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.CultureHistoryUIs
{
    public class CultureHistoryStatusPrefab : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI titleTxt;
        [SerializeField] private TextMeshProUGUI contentTxt;
        
        public void SetTitle(string title)
        {
            titleTxt.text = title;
        }
        
        public void SetContent(string content)
        {
            contentTxt.text = content;
        }
    }
}

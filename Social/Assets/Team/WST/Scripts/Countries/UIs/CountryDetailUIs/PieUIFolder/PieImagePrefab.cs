using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs.CountryDetailUIs.PieUIFolder
{
    public class PieImagePrefab : MonoBehaviour
    {
        [SerializeField] private Image pieImage;

        public void SetFillAmount(float fillAmount, Color color)
        {
            pieImage.fillAmount = fillAmount;
            pieImage.color = color;
        }
    }
}
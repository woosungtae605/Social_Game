using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CulturePercentElement : MonoBehaviour
    {
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private Image image;
        
        public void SetData(float ratio, Color color)
        {
            layoutElement.minWidth = 0f;
            layoutElement.preferredWidth = 0f;
            layoutElement.flexibleWidth = ratio;

            image.color = color;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class CanvasToggleButton : MonoBehaviour
    {
        [SerializeField] private Button toggleButton;
        [SerializeField] private GameObject targetCanvas;

        private void Awake()
        {
            if (toggleButton != null)
                toggleButton.onClick.AddListener(Toggle);
        }

        private void OnDestroy()
        {
            if (toggleButton != null)
                toggleButton.onClick.RemoveListener(Toggle);
        }

        public void Toggle()
        {
            if (targetCanvas != null)
                targetCanvas.SetActive(!targetCanvas.activeSelf);
        }
    }
}

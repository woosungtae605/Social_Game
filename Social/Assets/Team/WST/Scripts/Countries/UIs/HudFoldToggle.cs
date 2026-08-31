using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class HudFoldToggle : MonoBehaviour
    {
        [SerializeField] private Button foldButton;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private GameObject[] foldTargets;

        [SerializeField] private string expandedLabel = "접기";
        [SerializeField] private string collapsedLabel = "펼치기";
        [SerializeField] private bool startExpanded = true;

        private bool _isExpanded;

        private void Awake()
        {
            _isExpanded = startExpanded;
            if (foldButton != null)
                foldButton.onClick.AddListener(Toggle);

            Apply();
        }

        private void OnDestroy()
        {
            if (foldButton != null)
                foldButton.onClick.RemoveListener(Toggle);
        }

        private void Toggle()
        {
            _isExpanded = !_isExpanded;
            Apply();
        }

        private void Apply()
        {
            if (foldTargets != null)
            {
                for (int i = 0; i < foldTargets.Length; i++)
                {
                    if (foldTargets[i] != null)
                        foldTargets[i].SetActive(_isExpanded);
                }
            }

            if (label != null)
                label.text = _isExpanded ? expandedLabel : collapsedLabel;
        }
    }
}

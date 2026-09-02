using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Title
{
    public class TitleOptionsPanel : MonoBehaviour
    {
        private const string VolumePrefKey = "Title.MasterVolume";

        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeValueText;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            float volume = PlayerPrefs.GetFloat(VolumePrefKey, AudioListener.volume);
            ApplyVolume(volume, writePrefs: false);

            if (volumeSlider != null)
            {
                volumeSlider.minValue = 0f;
                volumeSlider.maxValue = 1f;
                volumeSlider.SetValueWithoutNotify(volume);
                volumeSlider.onValueChanged.AddListener(HandleVolumeChanged);
            }

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            if (volumeSlider != null)
                volumeSlider.onValueChanged.RemoveListener(HandleVolumeChanged);
            if (closeButton != null)
                closeButton.onClick.RemoveListener(Hide);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void HandleVolumeChanged(float value)
        {
            ApplyVolume(value, writePrefs: true);
        }

        private void ApplyVolume(float value, bool writePrefs)
        {
            float clamped = Mathf.Clamp01(value);
            AudioListener.volume = clamped;

            if (volumeValueText != null)
                volumeValueText.text = $"{Mathf.RoundToInt(clamped * 100f)}%";

            if (writePrefs)
            {
                PlayerPrefs.SetFloat(VolumePrefKey, clamped);
                PlayerPrefs.Save();
            }
        }
    }
}

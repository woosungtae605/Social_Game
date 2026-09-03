using DevLib.EventChannelSystem;
using DevLib.SoundSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Settings
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private EventChannelSO soundChannel;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeValueText;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            float volume = PlayerPrefs.GetFloat(SoundEvents.MasterVolumePrefKey, 1f);
            SetVolumeLabel(volume);

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
            SetVolumeLabel(value);
            if (soundChannel == null)
                return;

            soundChannel.RaiseEvent(SoundEvents.SetSoundVolumeEvent.Init(value));
        }

        private void SetVolumeLabel(float volume)
        {
            if (volumeValueText != null)
                volumeValueText.text = $"{Mathf.RoundToInt(Mathf.Clamp01(volume) * 100f)}%";
        }
    }
}

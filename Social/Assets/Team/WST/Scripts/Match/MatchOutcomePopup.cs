using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Match
{
    public class MatchOutcomePopup : MonoBehaviour
    {
        [SerializeField] private GameObject root;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button titleButton;
        [SerializeField] private string clearedText = "클리어";
        [SerializeField] private string failedText = "실패";
        [SerializeField] private string gameSceneName = "WSTScene";
        [SerializeField] private string titleSceneName = "TitleScene";

        private void Awake()
        {
            if (root != null)
                root.SetActive(false);

            if (retryButton != null)
                retryButton.onClick.AddListener(HandleRetry);

            if (titleButton != null)
                titleButton.onClick.AddListener(HandleTitle);

            Bus<MatchEndedEvent>.OnEvent += HandleMatchEnded;
        }

        private void OnDestroy()
        {
            if (retryButton != null)
                retryButton.onClick.RemoveListener(HandleRetry);

            if (titleButton != null)
                titleButton.onClick.RemoveListener(HandleTitle);

            Bus<MatchEndedEvent>.OnEvent -= HandleMatchEnded;
        }

        private void HandleMatchEnded(MatchEndedEvent evt)
        {
            if (resultText != null)
                resultText.text = evt.Result == MatchResult.Cleared ? clearedText : failedText;

            if (root != null)
            {
                root.SetActive(true);
                var rootRect = root.GetComponent<RectTransform>();
                if (rootRect != null)
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rootRect);
            }

            Time.timeScale = 0f;
        }

        private void HandleRetry()
        {
            Time.timeScale = 1f;
            FadeManager.LoadSceneOrFallback(gameSceneName);
        }

        private void HandleTitle()
        {
            Time.timeScale = 1f;
            FadeManager.LoadSceneOrFallback(titleSceneName);
        }
    }
}

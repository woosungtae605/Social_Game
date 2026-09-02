using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team.WST.Scripts.Settings
{
    public class GameSettingsMenu : MonoBehaviour
    {
        [SerializeField] private Button openButton;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private Button returnToTitleButton;
        [SerializeField] private string titleSceneName = "TitleScene";

        private void Awake()
        {
            if (openButton != null)
                openButton.onClick.AddListener(HandleOpen);
            if (returnToTitleButton != null)
                returnToTitleButton.onClick.AddListener(HandleReturnToTitle);
        }

        private void OnDestroy()
        {
            if (openButton != null)
                openButton.onClick.RemoveListener(HandleOpen);
            if (returnToTitleButton != null)
                returnToTitleButton.onClick.RemoveListener(HandleReturnToTitle);
        }

        private void HandleOpen()
        {
            if (settingsPanel != null)
                settingsPanel.Show();
        }

        private void HandleReturnToTitle()
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team.WST.Scripts.Title
{
    public class TitleMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TitleOptionsPanel optionsPanel;
        [SerializeField] private string gameSceneName = "WSTScene";

        private void Awake()
        {
            if (startButton != null)
                startButton.onClick.AddListener(HandleStart);
            if (optionsButton != null)
                optionsButton.onClick.AddListener(HandleOptions);
            if (exitButton != null)
                exitButton.onClick.AddListener(HandleExit);
        }

        private void OnDestroy()
        {
            if (startButton != null)
                startButton.onClick.RemoveListener(HandleStart);
            if (optionsButton != null)
                optionsButton.onClick.RemoveListener(HandleOptions);
            if (exitButton != null)
                exitButton.onClick.RemoveListener(HandleExit);
        }

        private void HandleStart()
        {
            SceneManager.LoadScene(gameSceneName);
        }

        private void HandleOptions()
        {
            if (optionsPanel != null)
                optionsPanel.Show();
        }

        private void HandleExit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

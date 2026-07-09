using UnityEngine;
using UnityEngine.UI;

public class BoardPanelController : MonoBehaviour
{
    [SerializeField] private GameObject boardPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        boardPanel.SetActive(false);

        openButton.onClick.AddListener(OpenBoard);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseBoard);
    }

    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(OpenBoard);

        if (closeButton != null)
            closeButton.onClick.RemoveListener(CloseBoard);
    }

    private void OpenBoard()
    {
        boardPanel.SetActive(true);
    }

    private void CloseBoard()
    {
        boardPanel.SetActive(false);
    }
}
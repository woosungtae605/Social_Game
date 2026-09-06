using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class ChatCanvasOpener : MonoBehaviour
    {
        [SerializeField] private Button openButton;
        [SerializeField] private GameObject chatCanvas;

        private void Awake()
        {
            if (openButton != null)
                openButton.onClick.AddListener(HandleOpen);
        }

        private void OnDestroy()
        {
            if (openButton != null)
                openButton.onClick.RemoveListener(HandleOpen);
        }

        private void HandleOpen()
        {
            if (chatCanvas != null)
                chatCanvas.SetActive(!chatCanvas.activeSelf);
        }
    }
}

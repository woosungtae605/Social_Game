using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class ChatCanvasOpener : MonoBehaviour
    {
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject chatCanvas;

        private void Awake()
        {
            if (openButton != null)
                openButton.onClick.AddListener(HandleOpen);

            if (closeButton != null)
                closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            if (openButton != null)
                openButton.onClick.RemoveListener(HandleOpen);

            if (closeButton != null)
                closeButton.onClick.RemoveListener(Close);
        }

        private void HandleOpen()
        {
            if (chatCanvas != null)
                chatCanvas.SetActive(!chatCanvas.activeSelf);
        }

        public void Close()
        {
            if (chatCanvas != null)
                chatCanvas.SetActive(false);
        }
    }
}

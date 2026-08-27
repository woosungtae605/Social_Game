using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardSelectItem : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image iconImage;

        private BoardSo board;
        private BoardPanelController panelController;

        private void Awake()
        {
            if (button == null)
                button = GetComponent<Button>();

            if (nameText == null)
                nameText = GetComponentInChildren<TMP_Text>(true);

            if (iconImage == null)
                iconImage = GetComponent<Image>();

            if (button != null)
                button.onClick.AddListener(OpenBoard);
        }

        public void Setup(BoardSo boardSo, BoardPanelController controller)
        {
            board = boardSo;
            panelController = controller;

            if (nameText != null && board != null)
                nameText.text = board.DisplayName;

            if (iconImage != null && board != null && board.Icon != null)
                iconImage.sprite = board.Icon;
        }

        private void OpenBoard()
        {
            if (board == null || panelController == null)
                return;

            panelController.OpenBoard(board);
        }

        private void OnDestroy()
        {
            if (button != null)
                button.onClick.RemoveListener(OpenBoard);
        }
    }
}

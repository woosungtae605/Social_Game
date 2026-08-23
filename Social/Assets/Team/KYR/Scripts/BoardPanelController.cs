using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject boardPanel;
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private BoardManager boardManager;

        private void Awake()
        {
            boardPanel.SetActive(false);

            if (boardManager == null)
                boardManager = GetComponent<BoardManager>();

            if (closeButton != null)
                closeButton.onClick.AddListener(CloseBoard);

            BindBoardButtons();
        }

        public void OpenBoard(BoardSo board)
        {
            if (board == null || boardManager == null)
                return;

            boardManager.SelectBoard(board);
            boardPanel.SetActive(true);
        }

        private void BindBoardButtons()
        {
            if (openButton == null || boardManager == null)
                return;

            Transform parent = openButton.transform.parent;
            Button[] existingButtons = parent.GetComponentsInChildren<Button>(true);
            if (existingButtons.Length == 0)
                return;

            BoardSo[] boards = boardManager.Boards;
            int boundCount = 0;

            for (int i = 0; i < boards.Length; i++)
            {
                BoardSo board = boards[i];
                if (board == null)
                    continue;

                Button button;
                if (boundCount < existingButtons.Length)
                {
                    button = existingButtons[boundCount];
                }
                else
                {
                    Button template = existingButtons[0];
                    button = Instantiate(template, parent);

                    RectTransform lastRect = existingButtons[existingButtons.Length - 1].transform as RectTransform;
                    RectTransform cloneRect = button.transform as RectTransform;
                    if (lastRect != null && cloneRect != null)
                    {
                        float offset = 265f * (boundCount - existingButtons.Length + 1);
                        cloneRect.anchoredPosition = lastRect.anchoredPosition + new Vector2(offset, 0f);
                    }
                }

                BoardSelectItem item = button.GetComponent<BoardSelectItem>();
                if (item == null)
                    item = button.gameObject.AddComponent<BoardSelectItem>();

                item.Setup(board, this);
                button.gameObject.SetActive(true);
                boundCount++;
            }

            for (int i = boundCount; i < existingButtons.Length; i++)
                existingButtons[i].gameObject.SetActive(false);
        }

        private void CloseBoard()
        {
            boardPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveListener(CloseBoard);
        }
    }
}

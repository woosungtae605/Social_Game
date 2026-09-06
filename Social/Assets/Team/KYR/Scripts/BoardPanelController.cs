using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject boardPanel;
        [SerializeField] private GameObject emptyState;
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button allPostsTab;
        [SerializeField] private Button conceptPostsTab;
        [SerializeField] private TMP_Text allPostsLabel;
        [SerializeField] private TMP_Text conceptPostsLabel;
        [SerializeField] private GameObject allTabUnderline;
        [SerializeField] private GameObject conceptTabUnderline;
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private BoardPostDetailView postDetailView;

        private static readonly Color ActiveTab = new Color(59f / 255f, 72f / 255f, 144f / 255f, 1f);
        private static readonly Color InactiveTab = new Color(0.45f, 0.45f, 0.48f, 1f);

        private void Awake()
        {
            if (boardPanel != null)
                boardPanel.SetActive(false);

            if (emptyState != null)
                emptyState.SetActive(true);

            if (boardManager == null)
                boardManager = GetComponent<BoardManager>();

            if (postDetailView == null)
                postDetailView = GetComponent<BoardPostDetailView>();

            if (closeButton != null)
                closeButton.onClick.AddListener(CloseBoard);

            if (allPostsTab != null)
                allPostsTab.onClick.AddListener(ShowAllPosts);

            if (conceptPostsTab != null)
                conceptPostsTab.onClick.AddListener(ShowConceptPosts);

            BindBoardButtons();
            ApplyTabStyle(false);
        }

        public void OpenBoard(BoardSo board)
        {
            if (board == null || boardManager == null)
                return;

            if (postDetailView != null)
                postDetailView.Hide();

            boardManager.SelectBoard(board);

            if (emptyState != null)
                emptyState.SetActive(false);

            if (boardPanel != null)
                boardPanel.SetActive(true);

            ApplyTabStyle(false);
        }

        public void CloseBoard()
        {
            if (postDetailView != null && postDetailView.IsOpen)
            {
                postDetailView.Hide();
                return;
            }

            if (boardPanel != null)
                boardPanel.SetActive(false);

            if (emptyState != null)
                emptyState.SetActive(true);

            ApplyTabStyle(false);
        }

        private void ShowAllPosts()
        {
            if (boardManager != null)
                boardManager.ShowAllPosts();

            ApplyTabStyle(false);
        }

        private void ShowConceptPosts()
        {
            if (boardManager != null)
                boardManager.ShowConceptPosts();

            ApplyTabStyle(true);
        }

        private void ApplyTabStyle(bool conceptSelected)
        {
            if (allPostsLabel != null)
            {
                allPostsLabel.color = conceptSelected ? InactiveTab : ActiveTab;
                allPostsLabel.fontStyle = conceptSelected ? FontStyles.Normal : FontStyles.Bold;
            }

            if (conceptPostsLabel != null)
            {
                conceptPostsLabel.color = conceptSelected ? ActiveTab : InactiveTab;
                conceptPostsLabel.fontStyle = conceptSelected ? FontStyles.Bold : FontStyles.Normal;
            }

            if (allTabUnderline != null)
                allTabUnderline.SetActive(!conceptSelected);

            if (conceptTabUnderline != null)
                conceptTabUnderline.SetActive(conceptSelected);
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

        private void OnDestroy()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveListener(CloseBoard);

            if (allPostsTab != null)
                allPostsTab.onClick.RemoveListener(ShowAllPosts);

            if (conceptPostsTab != null)
                conceptPostsTab.onClick.RemoveListener(ShowConceptPosts);
        }
    }
}

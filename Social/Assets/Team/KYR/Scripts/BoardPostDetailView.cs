using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardPostDetailView : MonoBehaviour
    {
        private const float ImageSize = 180f;

        [SerializeField] private GameObject detailPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button recommendButton;
        [SerializeField] private Button dislikeButton;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text writerText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text bodyText;
        [SerializeField] private TMP_Text recommendCountText;
        [SerializeField] private TMP_Text dislikeCountText;
        [SerializeField] private Image recommendBox;
        [SerializeField] private Transform imageRow;
        [SerializeField] private BoardManager boardManager;

        private BoardPostData currentPost;
        private bool isOpen;

        private static readonly Color RecommendIdle = Color.white;
        private static readonly Color RecommendOn = new Color(0.91f, 0.93f, 0.98f, 1f);

        public bool IsOpen => isOpen;

        private void Awake()
        {
            if (boardManager == null)
                boardManager = GetComponent<BoardManager>();

            if (closeButton != null)
                closeButton.onClick.AddListener(Hide);

            if (deleteButton != null)
                deleteButton.onClick.AddListener(DeleteCurrentPost);

            if (recommendButton != null)
                recommendButton.onClick.AddListener(RecommendCurrentPost);

            if (dislikeButton != null)
                dislikeButton.onClick.AddListener(DislikeCurrentPost);

            if (detailPanel != null)
                detailPanel.SetActive(false);
        }

        public void Show(BoardPostData post)
        {
            if (post == null || detailPanel == null)
                return;

            currentPost = post;

            if (titleText != null)
                titleText.text = post.Title;

            if (writerText != null)
                writerText.text = post.Writer;

            if (dateText != null)
                dateText.text = post.CreatedAt.ToString("yyyy.MM.dd");

            if (bodyText != null)
                bodyText.text = post.Body;

            RefreshImages(post.Images);
            RefreshVoteState();

            detailPanel.SetActive(true);
            isOpen = true;
        }

        public void Hide()
        {
            if (detailPanel != null)
                detailPanel.SetActive(false);

            currentPost = null;
            isOpen = false;
        }

        public void RefreshVoteState()
        {
            if (currentPost == null)
                return;

            if (recommendCountText != null)
                recommendCountText.text = currentPost.RecommendCount.ToString();

            if (dislikeCountText != null)
                dislikeCountText.text = currentPost.DislikeCount.ToString();

            if (recommendBox != null)
                recommendBox.color = currentPost.IsConcept ? RecommendOn : RecommendIdle;
        }

        private void RecommendCurrentPost()
        {
            if (currentPost == null || boardManager == null)
                return;

            boardManager.RecommendConcept(currentPost);
        }

        private void DislikeCurrentPost()
        {
            if (currentPost == null || boardManager == null)
                return;

            boardManager.AddDislike(currentPost);
        }

        private void DeleteCurrentPost()
        {
            if (currentPost == null || boardManager == null)
                return;

            boardManager.DeletePost(currentPost);
        }

        private void RefreshImages(Sprite[] images)
        {
            if (imageRow == null)
                return;

            for (int i = imageRow.childCount - 1; i >= 0; i--)
            {
                GameObject child = imageRow.GetChild(i).gameObject;
                child.SetActive(false);
                child.transform.SetParent(null);
                Destroy(child);
            }

            int shown = 0;
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i] == null)
                    continue;

                CreateImage(images[i]);
                shown++;
            }

            imageRow.gameObject.SetActive(shown > 0);
        }

        private void CreateImage(Sprite sprite)
        {
            GameObject imageObject = new GameObject("PostImage", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(LayoutElement));
            imageObject.transform.SetParent(imageRow, false);

            LayoutElement layout = imageObject.GetComponent<LayoutElement>();
            layout.minWidth = ImageSize;
            layout.minHeight = ImageSize;
            layout.preferredWidth = ImageSize;
            layout.preferredHeight = ImageSize;
            layout.flexibleWidth = 0f;
            layout.flexibleHeight = 0f;

            Image image = imageObject.GetComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            image.raycastTarget = false;
        }

        private void OnDestroy()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveListener(Hide);

            if (deleteButton != null)
                deleteButton.onClick.RemoveListener(DeleteCurrentPost);

            if (recommendButton != null)
                recommendButton.onClick.RemoveListener(RecommendCurrentPost);

            if (dislikeButton != null)
                dislikeButton.onClick.RemoveListener(DislikeCurrentPost);
        }
    }
}

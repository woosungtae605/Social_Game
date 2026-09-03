using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardPostItem : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button deleteButton;
        [SerializeField] private Button conceptToggleButton;
        [SerializeField] private TMP_Text conceptToggleText;
        [SerializeField] private Button openButton;

        [Header("Post Data")]
        [SerializeField] private TMP_Text writerText;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text viewCountText;
        [SerializeField] private TMP_Text dateText;

        private BoardPostData postData;
        private BoardManager boardManager;

        private void Awake()
        {
            deleteButton.onClick.AddListener(DeletePost);
            conceptToggleButton.onClick.AddListener(ToggleConcept);

            if (openButton == null)
                openButton = GetComponent<Button>();

            if (openButton == null)
            {
                openButton = gameObject.AddComponent<Button>();
                openButton.targetGraphic = GetComponent<Image>();
            }

            openButton.onClick.AddListener(OpenPost);
        }

        public void Setup(BoardPostData data, BoardManager manager)
        {
            postData = data;
            boardManager = manager;

            writerText.text = postData.Writer;
            titleText.text = postData.Title;
            viewCountText.text = postData.ViewCount.ToString("N0");
            dateText.text = postData.CreatedAt.ToString("yyyy.MM.dd");

            UpdateConceptButton();
        }

        private void UpdateConceptButton()
        {
            conceptToggleText.text = postData.IsConcept ? "↓" : "✓";
        }

        private void OpenPost()
        {
            if (postData == null || boardManager == null)
                return;

            boardManager.OpenPost(postData);
            viewCountText.text = postData.ViewCount.ToString("N0");
        }

        private void DeletePost()
        {
            if (postData == null || boardManager == null)
                return;

            boardManager.DeletePost(postData);
        }

        private void ToggleConcept()
        {
            if (postData == null || boardManager == null)
                return;

            boardManager.ToggleConcept(postData);
        }

        private void OnDestroy()
        {
            deleteButton.onClick.RemoveListener(DeletePost);
            conceptToggleButton.onClick.RemoveListener(ToggleConcept);

            if (openButton != null)
                openButton.onClick.RemoveListener(OpenPost);
        }
    }
}
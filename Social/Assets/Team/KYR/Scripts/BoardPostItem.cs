using System;
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
        [SerializeField] private TMP_Text numberText;
        [SerializeField] private TMP_Text writerText;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text viewCountText;
        [SerializeField] private TMP_Text dateText;
        [SerializeField] private TMP_Text recommendText;

        private BoardPostData postData;
        private BoardManager boardManager;

        private void Awake()
        {
            if (deleteButton != null)
                deleteButton.onClick.AddListener(DeletePost);

            if (conceptToggleButton != null)
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

            if (numberText != null)
                numberText.text = ((Mathf.Abs(postData.Title.GetHashCode()) % 900000) + 100000).ToString();

            writerText.text = postData.Writer;
            titleText.text = postData.Title;
            viewCountText.text = postData.ViewCount.ToString();
            dateText.text = FormatDate(postData.CreatedAt);

            if (recommendText != null)
                recommendText.text = postData.RecommendCount.ToString();

            UpdateConceptButton();
        }

        private void UpdateConceptButton()
        {
            if (conceptToggleText == null || postData == null)
                return;

            conceptToggleText.text = postData.IsConcept ? "↓" : "✓";
        }

        private static string FormatDate(DateTime createdAt)
        {
            if (createdAt.Date == DateTime.Today)
                return createdAt.ToString("HH:mm");

            return createdAt.ToString("yy.MM.dd");
        }

        private void OpenPost()
        {
            if (postData == null || boardManager == null)
                return;

            boardManager.OpenPost(postData);
            viewCountText.text = postData.ViewCount.ToString();
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
            if (deleteButton != null)
                deleteButton.onClick.RemoveListener(DeletePost);

            if (conceptToggleButton != null)
                conceptToggleButton.onClick.RemoveListener(ToggleConcept);

            if (openButton != null)
                openButton.onClick.RemoveListener(OpenPost);
        }
    }
}

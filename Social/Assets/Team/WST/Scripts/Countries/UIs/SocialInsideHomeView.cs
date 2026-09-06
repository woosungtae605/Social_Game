using System;
using Team.KYR.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class SocialInsideHomeView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField searchField;
        [SerializeField] private Button searchButton;
        [SerializeField] private Button allGalleriesButton;
        [SerializeField] private TMP_Text sectionTitle;
        [SerializeField] private Transform listRoot;
        [SerializeField] private BoardPanelController panelController;
        [SerializeField] private BoardManager boardManager;

        private void Awake()
        {
            if (panelController == null)
                panelController = GetComponent<BoardPanelController>();

            if (boardManager == null)
                boardManager = GetComponent<BoardManager>();
        }

        private void Start()
        {
            if (searchField != null)
            {
                searchField.onValueChanged.AddListener(HandleSearchChanged);
                searchField.onSubmit.AddListener(HandleSearchSubmit);
            }

            if (searchButton != null)
                searchButton.onClick.AddListener(TryEnterGallery);

            if (allGalleriesButton != null)
                allGalleriesButton.onClick.AddListener(ShowAllGalleries);

            RefreshList();
        }

        private void OnDestroy()
        {
            if (searchField != null)
            {
                searchField.onValueChanged.RemoveListener(HandleSearchChanged);
                searchField.onSubmit.RemoveListener(HandleSearchSubmit);
            }

            if (searchButton != null)
                searchButton.onClick.RemoveListener(TryEnterGallery);

            if (allGalleriesButton != null)
                allGalleriesButton.onClick.RemoveListener(ShowAllGalleries);
        }

        private void HandleSearchChanged(string _)
        {
            RefreshList();
        }

        private void HandleSearchSubmit(string _)
        {
            TryEnterGallery();
        }

        private void ShowAllGalleries()
        {
            if (searchField != null)
                searchField.SetTextWithoutNotify(string.Empty);

            if (panelController != null)
                panelController.CloseBoard();

            RefreshList();
        }

        private void TryEnterGallery()
        {
            string query = searchField != null ? searchField.text.Trim() : string.Empty;
            if (query.Length == 0)
                return;

            RefreshList();

            BoardSelectItem firstVisible = null;
            BoardSelectItem exact = null;
            if (listRoot != null)
            {
                BoardSelectItem[] items = listRoot.GetComponentsInChildren<BoardSelectItem>(false);
                for (int i = 0; i < items.Length; i++)
                {
                    if (!items[i].gameObject.activeSelf)
                        continue;

                    if (firstVisible == null)
                        firstVisible = items[i];

                    TMP_Text label = items[i].GetComponentInChildren<TMP_Text>(true);
                    if (label != null && string.Equals(label.text, query, StringComparison.OrdinalIgnoreCase))
                    {
                        exact = items[i];
                        break;
                    }
                }
            }

            BoardSelectItem chosen = exact != null ? exact : firstVisible;
            if (chosen != null)
                chosen.GetComponent<Button>().onClick.Invoke();
        }

        private void RefreshList()
        {
            if (listRoot == null)
                return;

            string query = searchField != null ? searchField.text.Trim() : string.Empty;
            int visible = 0;
            for (int i = 0; i < listRoot.childCount; i++)
            {
                Transform child = listRoot.GetChild(i);
                TMP_Text label = child.GetComponentInChildren<TMP_Text>(true);
                string name = label != null ? label.text : child.name;
                bool show = query.Length == 0 || name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                child.gameObject.SetActive(show);
                if (show)
                    visible++;
            }

            if (sectionTitle != null)
                sectionTitle.text = "게시판 (" + visible + ")";
        }
    }
}

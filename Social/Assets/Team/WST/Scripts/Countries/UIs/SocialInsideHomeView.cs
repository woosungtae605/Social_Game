using System;
using System.Collections.Generic;
using Team.KYR.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Countries.UIs
{
    public class SocialInsideHomeView : MonoBehaviour
    {
        private static readonly Color Navy = new Color(59f / 255f, 72f / 255f, 144f / 255f, 1f);
        private static readonly Color HeaderGray = new Color(0.94f, 0.94f, 0.94f, 1f);
        private static readonly Color LineGray = new Color(0.82f, 0.82f, 0.82f, 1f);
        private static readonly Color TextDark = new Color(0.2f, 0.22f, 0.28f, 1f);

        [SerializeField] private GameObject socialInsideToHide;
        [SerializeField] private BoardPanelController panelController;
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private TMP_FontAsset fontAsset;

        private TMP_InputField searchField;
        private TMP_Text sectionTitle;
        private Transform listRoot;
        private readonly List<BoardSo> visibleBoards = new List<BoardSo>();

        private void Awake()
        {
            if (panelController == null)
                panelController = GetComponent<BoardPanelController>();

            if (boardManager == null)
                boardManager = GetComponent<BoardManager>();
        }

        private void Start()
        {
            if (socialInsideToHide != null)
                socialInsideToHide.SetActive(false);

            BuildHome();
            RefreshList();
        }

        private void OnDestroy()
        {
            if (searchField != null)
            {
                searchField.onValueChanged.RemoveListener(HandleSearchChanged);
                searchField.onSubmit.RemoveListener(HandleSearchSubmit);
            }
        }

        private void BuildHome()
        {
            GameObject home = CreateUiObject("DcHome", transform, typeof(Image));
            Stretch(home.GetComponent<RectTransform>());
            home.transform.SetAsFirstSibling();

            Image homeImage = home.GetComponent<Image>();
            homeImage.color = Color.white;
            homeImage.raycastTarget = true;

            RectTransform header = CreateHeader(home.transform);
            CreateBody(home.transform, header);
        }

        private RectTransform CreateHeader(Transform parent)
        {
            GameObject headerObject = CreateUiObject("Header", parent, typeof(Image));
            RectTransform header = headerObject.GetComponent<RectTransform>();
            header.anchorMin = new Vector2(0f, 1f);
            header.anchorMax = new Vector2(1f, 1f);
            header.pivot = new Vector2(0.5f, 1f);
            header.anchoredPosition = Vector2.zero;
            header.sizeDelta = new Vector2(0f, 100f);
            headerObject.GetComponent<Image>().color = Color.white;

            GameObject line = CreateUiObject("HeaderLine", header, typeof(Image));
            RectTransform lineRect = line.GetComponent<RectTransform>();
            lineRect.anchorMin = new Vector2(0f, 0f);
            lineRect.anchorMax = new Vector2(1f, 0f);
            lineRect.pivot = new Vector2(0.5f, 0f);
            lineRect.anchoredPosition = Vector2.zero;
            lineRect.sizeDelta = new Vector2(0f, 3f);
            line.GetComponent<Image>().color = Navy;

            TMP_Text logo = CreateLabel(header, "Logo", "SocialInside", 40f, Color.black, TextAlignmentOptions.MidlineLeft);
            logo.fontStyle = FontStyles.Italic | FontStyles.Bold;
            RectTransform logoRect = logo.rectTransform;
            logoRect.anchorMin = new Vector2(0f, 0.5f);
            logoRect.anchorMax = new Vector2(0f, 0.5f);
            logoRect.pivot = new Vector2(0f, 0.5f);
            logoRect.anchoredPosition = new Vector2(40f, 4f);
            logoRect.sizeDelta = new Vector2(310f, 50f);
            logo.textWrappingMode = TextWrappingModes.NoWrap;

            TMP_Text gallery = CreateLabel(header, "GalleryWord", "갤러리", 22f, Navy, TextAlignmentOptions.MidlineLeft);
            RectTransform galleryRect = gallery.rectTransform;
            galleryRect.anchorMin = new Vector2(0f, 0.5f);
            galleryRect.anchorMax = new Vector2(0f, 0.5f);
            galleryRect.pivot = new Vector2(0f, 0.5f);
            galleryRect.anchoredPosition = new Vector2(360f, 2f);
            galleryRect.sizeDelta = new Vector2(80f, 40f);
            gallery.textWrappingMode = TextWrappingModes.NoWrap;

            CreateSearchBar(header);
            return header;
        }

        private void CreateSearchBar(Transform header)
        {
            GameObject frame = CreateUiObject("SearchFrame", header, typeof(Image), typeof(HorizontalLayoutGroup));
            RectTransform frameRect = frame.GetComponent<RectTransform>();
            frameRect.anchorMin = new Vector2(1f, 0.5f);
            frameRect.anchorMax = new Vector2(1f, 0.5f);
            frameRect.pivot = new Vector2(1f, 0.5f);
            frameRect.anchoredPosition = new Vector2(-40f, 0f);
            frameRect.sizeDelta = new Vector2(520f, 42f);
            frame.GetComponent<Image>().color = Navy;

            HorizontalLayoutGroup frameLayout = frame.GetComponent<HorizontalLayoutGroup>();
            frameLayout.padding = new RectOffset(3, 3, 3, 3);
            frameLayout.spacing = 0f;
            frameLayout.childAlignment = TextAnchor.MiddleLeft;
            frameLayout.childControlWidth = true;
            frameLayout.childControlHeight = true;
            frameLayout.childForceExpandWidth = false;
            frameLayout.childForceExpandHeight = true;

            GameObject inputObject = CreateUiObject("SearchInput", frame.transform, typeof(Image), typeof(TMP_InputField), typeof(LayoutElement));
            inputObject.GetComponent<Image>().color = Color.white;
            LayoutElement inputLayout = inputObject.GetComponent<LayoutElement>();
            inputLayout.flexibleWidth = 1f;
            inputLayout.preferredWidth = 430f;
            inputLayout.minHeight = 36f;
            inputLayout.preferredHeight = 36f;

            RectTransform textArea = CreateUiObject("TextArea", inputObject.transform, typeof(RectMask2D)).GetComponent<RectTransform>();
            Stretch(textArea);
            textArea.offsetMin = new Vector2(10f, 2f);
            textArea.offsetMax = new Vector2(-8f, -2f);

            TMP_Text placeholder = CreateLabel(textArea, "Placeholder", "갤러리 & 통합검색", 18f, new Color(0.6f, 0.6f, 0.6f, 1f), TextAlignmentOptions.MidlineLeft);
            Stretch(placeholder.rectTransform);
            placeholder.textWrappingMode = TextWrappingModes.NoWrap;

            TMP_Text inputText = CreateLabel(textArea, "Text", string.Empty, 18f, Color.black, TextAlignmentOptions.MidlineLeft);
            Stretch(inputText.rectTransform);
            inputText.textWrappingMode = TextWrappingModes.NoWrap;

            searchField = inputObject.GetComponent<TMP_InputField>();
            searchField.textViewport = textArea;
            searchField.textComponent = inputText;
            searchField.placeholder = placeholder;
            searchField.fontAsset = ResolveFont();
            searchField.pointSize = 18f;
            searchField.lineType = TMP_InputField.LineType.SingleLine;
            searchField.onValueChanged.AddListener(HandleSearchChanged);
            searchField.onSubmit.AddListener(HandleSearchSubmit);

            GameObject buttonObject = CreateUiObject("SearchButton", frame.transform, typeof(Image), typeof(Button), typeof(LayoutElement));
            buttonObject.GetComponent<Image>().color = Navy;
            LayoutElement buttonLayout = buttonObject.GetComponent<LayoutElement>();
            buttonLayout.preferredWidth = 72f;
            buttonLayout.minWidth = 72f;
            buttonLayout.minHeight = 36f;
            buttonLayout.preferredHeight = 36f;
            buttonLayout.flexibleWidth = 0f;

            Button searchButton = buttonObject.GetComponent<Button>();
            searchButton.targetGraphic = buttonObject.GetComponent<Image>();
            searchButton.onClick.AddListener(TryEnterGallery);

            TMP_Text searchLabel = CreateLabel(buttonObject.transform, "Label", "검색", 18f, Color.white, TextAlignmentOptions.Center);
            Stretch(searchLabel.rectTransform);
            searchLabel.textWrappingMode = TextWrappingModes.NoWrap;
        }

        private void CreateBody(Transform parent, RectTransform header)
        {
            GameObject body = CreateUiObject("Body", parent, typeof(Image));
            RectTransform bodyRect = body.GetComponent<RectTransform>();
            bodyRect.anchorMin = Vector2.zero;
            bodyRect.anchorMax = Vector2.one;
            bodyRect.offsetMin = Vector2.zero;
            bodyRect.offsetMax = new Vector2(0f, -header.sizeDelta.y);
            body.GetComponent<Image>().color = new Color(0.96f, 0.96f, 0.96f, 1f);

            GameObject sidebar = CreateUiObject("Sidebar", body.transform, typeof(Image), typeof(VerticalLayoutGroup));
            RectTransform sidebarRect = sidebar.GetComponent<RectTransform>();
            sidebarRect.anchorMin = new Vector2(0f, 0f);
            sidebarRect.anchorMax = new Vector2(0f, 1f);
            sidebarRect.pivot = new Vector2(0f, 1f);
            sidebarRect.offsetMin = Vector2.zero;
            sidebarRect.offsetMax = new Vector2(300f, 0f);
            sidebar.GetComponent<Image>().color = Color.white;

            VerticalLayoutGroup sidebarLayout = sidebar.GetComponent<VerticalLayoutGroup>();
            sidebarLayout.padding = new RectOffset(16, 16, 16, 16);
            sidebarLayout.spacing = 8f;
            sidebarLayout.childAlignment = TextAnchor.UpperLeft;
            sidebarLayout.childControlWidth = true;
            sidebarLayout.childControlHeight = true;
            sidebarLayout.childForceExpandWidth = true;
            sidebarLayout.childForceExpandHeight = false;

            GameObject allButtonObject = CreateUiObject("AllGalleriesButton", sidebar.transform, typeof(Image), typeof(Button), typeof(LayoutElement));
            allButtonObject.GetComponent<Image>().color = Navy;
            LayoutElement allLayout = allButtonObject.GetComponent<LayoutElement>();
            allLayout.preferredHeight = 40f;
            allLayout.minHeight = 40f;
            Button allButton = allButtonObject.GetComponent<Button>();
            allButton.targetGraphic = allButtonObject.GetComponent<Image>();
            allButton.onClick.AddListener(ShowAllGalleries);
            TMP_Text allLabel = CreateLabel(allButtonObject.transform, "Label", "갤러리 전체보기", 18f, Color.white, TextAlignmentOptions.Center);
            Stretch(allLabel.rectTransform);
            allLabel.textWrappingMode = TextWrappingModes.NoWrap;

            GameObject section = CreateUiObject("SectionHeader", sidebar.transform, typeof(Image), typeof(LayoutElement));
            section.GetComponent<Image>().color = HeaderGray;
            LayoutElement sectionLayout = section.GetComponent<LayoutElement>();
            sectionLayout.preferredHeight = 36f;
            sectionLayout.minHeight = 36f;
            sectionTitle = CreateLabel(section.transform, "Title", "게시판 (0)", 16f, Navy, TextAlignmentOptions.MidlineLeft);
            Stretch(sectionTitle.rectTransform);
            sectionTitle.rectTransform.offsetMin = new Vector2(12f, 0f);
            sectionTitle.rectTransform.offsetMax = new Vector2(-12f, 0f);
            sectionTitle.textWrappingMode = TextWrappingModes.NoWrap;

            GameObject list = CreateUiObject("BoardList", sidebar.transform, typeof(VerticalLayoutGroup), typeof(LayoutElement), typeof(ContentSizeFitter));
            LayoutElement listLayout = list.GetComponent<LayoutElement>();
            listLayout.flexibleHeight = 1f;
            listLayout.flexibleWidth = 1f;
            VerticalLayoutGroup listGroup = list.GetComponent<VerticalLayoutGroup>();
            listGroup.spacing = 0f;
            listGroup.childAlignment = TextAnchor.UpperLeft;
            listGroup.childControlWidth = true;
            listGroup.childControlHeight = true;
            listGroup.childForceExpandWidth = true;
            listGroup.childForceExpandHeight = false;
            ContentSizeFitter fitter = list.GetComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            listRoot = list.transform;

            GameObject edge = CreateUiObject("SidebarEdge", sidebar.transform.parent, typeof(Image));
            RectTransform edgeRect = edge.GetComponent<RectTransform>();
            edgeRect.anchorMin = new Vector2(0f, 0f);
            edgeRect.anchorMax = new Vector2(0f, 1f);
            edgeRect.pivot = new Vector2(0f, 0.5f);
            edgeRect.anchoredPosition = new Vector2(300f, 0f);
            edgeRect.sizeDelta = new Vector2(1f, 0f);
            edge.GetComponent<Image>().color = LineGray;

            TMP_Text hint = CreateLabel(body.transform, "Hint", "갤러리 이름을 검색하거나 왼쪽 목록에서 고르세요.", 20f, new Color(0.45f, 0.45f, 0.5f, 1f), TextAlignmentOptions.Center);
            RectTransform hintRect = hint.rectTransform;
            hintRect.anchorMin = new Vector2(0f, 0f);
            hintRect.anchorMax = new Vector2(1f, 1f);
            hintRect.offsetMin = new Vector2(320f, 40f);
            hintRect.offsetMax = new Vector2(-40f, -40f);
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
                searchField.text = string.Empty;

            RefreshList();
        }

        private void TryEnterGallery()
        {
            string query = searchField != null ? searchField.text.Trim() : string.Empty;
            if (query.Length == 0)
                return;

            RefreshList();
            if (visibleBoards.Count == 0 || panelController == null)
                return;
            BoardSo exact = null;
            for (int i = 0; i < visibleBoards.Count; i++)
            {
                BoardSo board = visibleBoards[i];
                if (board != null && string.Equals(board.DisplayName, query, StringComparison.OrdinalIgnoreCase))
                {
                    exact = board;
                    break;
                }
            }

            panelController.OpenBoard(exact != null ? exact : visibleBoards[0]);
        }

        private void RefreshList()
        {
            visibleBoards.Clear();
            if (listRoot == null || boardManager == null)
                return;

            string query = searchField != null ? searchField.text.Trim() : string.Empty;
            BoardSo[] boards = boardManager.Boards;
            for (int i = 0; i < boards.Length; i++)
            {
                BoardSo board = boards[i];
                if (board == null)
                    continue;

                if (query.Length > 0 && board.DisplayName.IndexOf(query, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                visibleBoards.Add(board);
            }

            for (int i = listRoot.childCount - 1; i >= 0; i--)
                Destroy(listRoot.GetChild(i).gameObject);

            if (sectionTitle != null)
                sectionTitle.text = "게시판 (" + visibleBoards.Count + ")";

            for (int i = 0; i < visibleBoards.Count; i++)
                CreateBoardRow(visibleBoards[i]);
        }

        private void CreateBoardRow(BoardSo board)
        {
            GameObject row = CreateUiObject("BoardRow", listRoot, typeof(Image), typeof(Button), typeof(LayoutElement));
            Image background = row.GetComponent<Image>();
            background.color = Color.white;
            LayoutElement layout = row.GetComponent<LayoutElement>();
            layout.preferredHeight = 34f;
            layout.minHeight = 34f;

            Button button = row.GetComponent<Button>();
            button.targetGraphic = background;
            ColorBlock colors = button.colors;
            colors.highlightedColor = HeaderGray;
            colors.pressedColor = LineGray;
            colors.selectedColor = HeaderGray;
            button.colors = colors;

            BoardSo selected = board;
            button.onClick.AddListener(() =>
            {
                if (panelController != null)
                    panelController.OpenBoard(selected);
            });

            TMP_Text label = CreateLabel(row.transform, "Name", board.DisplayName, 17f, TextDark, TextAlignmentOptions.MidlineLeft);
            Stretch(label.rectTransform);
            label.rectTransform.offsetMin = new Vector2(12f, 0f);
            label.rectTransform.offsetMax = new Vector2(-8f, 0f);
            label.textWrappingMode = TextWrappingModes.NoWrap;
        }

        private TMP_Text CreateLabel(Transform parent, string name, string text, float size, Color color, TextAlignmentOptions align)
        {
            GameObject textObject = CreateUiObject(name, parent, typeof(TextMeshProUGUI));
            TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
            TMP_FontAsset font = ResolveFont();
            if (font != null)
                tmp.font = font;
            tmp.text = text;
            tmp.fontSize = size;
            tmp.color = color;
            tmp.alignment = align;
            tmp.raycastTarget = false;
            return tmp;
        }

        private TMP_FontAsset ResolveFont()
        {
            if (fontAsset != null)
                return fontAsset;

            return TMP_Settings.defaultFontAsset;
        }

        private static GameObject CreateUiObject(string name, Transform parent, params Type[] extraTypes)
        {
            Type[] types = new Type[extraTypes.Length + 2];
            types[0] = typeof(RectTransform);
            types[1] = typeof(CanvasRenderer);
            for (int i = 0; i < extraTypes.Length; i++)
                types[i + 2] = extraTypes[i];

            GameObject created = new GameObject(name, types);
            created.layer = 5;
            created.transform.SetParent(parent, false);
            return created;
        }

        private static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }
    }
}

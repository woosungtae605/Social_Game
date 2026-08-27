using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team.KYR.Scripts
{
    public class BoardPostDetailView : MonoBehaviour
    {
        private const float ImageSize = 180f;

        [SerializeField] private GameObject boardPanel;

        private GameObject detailPanel;
        private TMP_Text titleText;
        private TMP_Text writerText;
        private TMP_Text dateText;
        private TMP_Text bodyText;
        private Transform imageRow;
        private Button closeButton;
        private bool isOpen;

        public bool IsOpen => isOpen;

        public void Show(BoardPostData post)
        {
            if (post == null)
                return;

            EnsurePanel();

            titleText.text = post.Title;
            writerText.text = post.Writer;
            dateText.text = post.CreatedAt.ToString("yyyy.MM.dd");
            bodyText.text = post.Body;

            RefreshImages(post.Images);

            detailPanel.SetActive(true);
            isOpen = true;
        }

        public void Hide()
        {
            if (detailPanel != null)
                detailPanel.SetActive(false);

            isOpen = false;
        }

        private void RefreshImages(Sprite[] images)
        {
            for (int i = imageRow.childCount - 1; i >= 0; i--)
            {
                GameObject child = imageRow.GetChild(i).gameObject;
                child.SetActive(false);
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

        private void EnsurePanel()
        {
            if (detailPanel != null)
                return;

            Transform parent = boardPanel != null ? boardPanel.transform : transform;
            detailPanel = new GameObject("PostDetailPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            detailPanel.transform.SetParent(parent, false);
            detailPanel.transform.SetAsLastSibling();

            RectTransform panelRect = detailPanel.GetComponent<RectTransform>();
            Stretch(panelRect);

            Image background = detailPanel.GetComponent<Image>();
            background.color = Color.white;
            background.raycastTarget = true;

            closeButton = CreateCloseButton(detailPanel.transform);
            closeButton.onClick.AddListener(Hide);

            RectTransform contentRoot = CreateContentRoot(detailPanel.transform);
            titleText = CreateText(contentRoot, "Title", 42, false);
            titleText.fontStyle = FontStyles.Bold;

            writerText = CreateText(contentRoot, "Writer", 28, false);
            dateText = CreateText(contentRoot, "Date", 28, false);
            bodyText = CreateText(contentRoot, "Body", 30, true);

            LayoutElement bodyLayout = bodyText.gameObject.AddComponent<LayoutElement>();
            bodyLayout.minHeight = 80f;
            bodyLayout.flexibleWidth = 1f;
            bodyLayout.flexibleHeight = 0f;

            imageRow = CreateImageRow(contentRoot);

            detailPanel.SetActive(false);
        }

        private static Button CreateCloseButton(Transform parent)
        {
            GameObject buttonObject = new GameObject("DetailCloseBtn", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            buttonObject.transform.SetParent(parent, false);

            RectTransform rect = buttonObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = new Vector2(-40f, -40f);
            rect.sizeDelta = new Vector2(100f, 80f);

            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(1f, 0.45f, 0.37f, 1f);

            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;

            GameObject textObject = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            textObject.transform.SetParent(buttonObject.transform, false);
            Stretch(textObject.GetComponent<RectTransform>());

            TextMeshProUGUI label = textObject.GetComponent<TextMeshProUGUI>();
            ApplyFont(label);
            label.text = "X";
            label.fontSize = 36;
            label.alignment = TextAlignmentOptions.Center;
            label.color = Color.white;
            label.raycastTarget = false;
            label.textWrappingMode = TextWrappingModes.NoWrap;

            return button;
        }

        private static RectTransform CreateContentRoot(Transform parent)
        {
            GameObject scrollObject = new GameObject("DetailScroll", typeof(RectTransform), typeof(ScrollRect));
            scrollObject.transform.SetParent(parent, false);

            RectTransform scrollRectTransform = scrollObject.GetComponent<RectTransform>();
            scrollRectTransform.anchorMin = Vector2.zero;
            scrollRectTransform.anchorMax = Vector2.one;
            scrollRectTransform.offsetMin = new Vector2(80f, 60f);
            scrollRectTransform.offsetMax = new Vector2(-80f, -140f);

            GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(RectMask2D));
            viewport.transform.SetParent(scrollObject.transform, false);
            RectTransform viewportRect = viewport.GetComponent<RectTransform>();
            Stretch(viewportRect);
            Image viewportImage = viewport.GetComponent<Image>();
            viewportImage.color = Color.white;
            viewportImage.raycastTarget = true;

            GameObject content = new GameObject("Content", typeof(RectTransform), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            content.transform.SetParent(viewport.transform, false);

            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0f, 1f);
            contentRect.anchorMax = new Vector2(1f, 1f);
            contentRect.pivot = new Vector2(0.5f, 1f);
            contentRect.anchoredPosition = Vector2.zero;
            contentRect.sizeDelta = new Vector2(0f, 0f);

            VerticalLayoutGroup layout = content.GetComponent<VerticalLayoutGroup>();
            layout.spacing = 16f;
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;
            layout.padding = new RectOffset(0, 0, 0, 24);

            ContentSizeFitter fitter = content.GetComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            ScrollRect scroll = scrollObject.GetComponent<ScrollRect>();
            scroll.content = contentRect;
            scroll.viewport = viewportRect;
            scroll.horizontal = false;
            scroll.vertical = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;

            return contentRect;
        }

        private static Transform CreateImageRow(Transform parent)
        {
            GameObject row = new GameObject("ImageRow", typeof(RectTransform), typeof(HorizontalLayoutGroup), typeof(LayoutElement));
            row.transform.SetParent(parent, false);

            HorizontalLayoutGroup layout = row.GetComponent<HorizontalLayoutGroup>();
            layout.spacing = 16f;
            layout.childAlignment = TextAnchor.UpperLeft;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;

            LayoutElement rowLayout = row.GetComponent<LayoutElement>();
            rowLayout.minHeight = ImageSize;
            rowLayout.preferredHeight = ImageSize;
            rowLayout.flexibleWidth = 1f;
            rowLayout.flexibleHeight = 0f;

            return row.transform;
        }

        private static TMP_Text CreateText(Transform parent, string name, float fontSize, bool wrap)
        {
            GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            textObject.transform.SetParent(parent, false);

            TextMeshProUGUI tmp = textObject.GetComponent<TextMeshProUGUI>();
            ApplyFont(tmp);
            tmp.fontSize = fontSize;
            tmp.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            tmp.alignment = TextAlignmentOptions.MidlineLeft;
            tmp.raycastTarget = false;
            tmp.textWrappingMode = wrap ? TextWrappingModes.Normal : TextWrappingModes.NoWrap;
            tmp.overflowMode = wrap ? TextOverflowModes.Overflow : TextOverflowModes.Ellipsis;

            if (wrap)
            {
                ContentSizeFitter fitter = textObject.AddComponent<ContentSizeFitter>();
                fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            }

            return tmp;
        }

        private static void ApplyFont(TMP_Text tmp)
        {
            if (TMP_Settings.defaultFontAsset != null)
                tmp.font = TMP_Settings.defaultFontAsset;
        }

        private static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        private void OnDestroy()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveListener(Hide);
        }
    }
}

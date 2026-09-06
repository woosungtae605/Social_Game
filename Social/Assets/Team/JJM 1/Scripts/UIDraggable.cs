using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using CoreSystem.EffectSystem;
using DevLib.ModuleSystem;
using DevLib.ObjectPool.Runtime;
using DG.Tweening;
using JJM.Scripts.CoreSystem.Effect;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JJM.Scripts
{
    public class UIDraggable : ModuleOwner,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPoolable
    {
        [field: SerializeField]
        public float Uniqueness { get; private set; } = 50f;

        [Header("VFX")]
        [SerializeField] private AssetNameSo assetNameGreen;
        [SerializeField] private AssetNameSo assetNameRed;
        [SerializeField] private Transform yesTextBundle;
        
        [Header("Drag")]
        [SerializeField] private RectTransform dragArea;

        private static readonly List<UIDraggable> Draggables = new();

        private readonly Vector3[] _dragAreaCorners = new Vector3[4];
        private readonly Vector3[] _draggableCorners = new Vector3[4];

        private IVfxModule _vfxModule;
        public IVfxModule VfxModule => _vfxModule;

        [SerializeField] private TextMeshProUGUI _yesText;
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Image _image;
        private TextMeshProUGUI _text;

        protected override void Awake()
        {
            base.Awake();

            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>().rootCanvas;
            _image = GetComponent<Image>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _vfxModule = GetModule<IVfxModule>();

            _text.text =
                ((int)Uniqueness).ToString(CultureInfo.InvariantCulture);

            Debug.Assert(
                _vfxModule != null,
                "Vfx 모듈을 넣어주세요."
            );

            Debug.Assert(
                dragArea != null,
                "드래그 가능한 영역(Drag Area)을 넣어주세요."
            );
        }

        private void OnEnable()
        {
            if (!Draggables.Contains(this))
                Draggables.Add(this);
        }

        private void OnDisable()
        {
            Draggables.Remove(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition +=
                eventData.delta / _canvas.scaleFactor;

            ClampToDragArea();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            for (int i = 0; i < Draggables.Count; i++)
            {
                UIDraggable other = Draggables[i];

                if (other == this)
                    continue;

                if (!IsOverlap(
                        _rectTransform,
                        other._rectTransform))
                    continue;

                other.Merge(this);

                Destroy(gameObject);
                return;
            }
        }

        private void Merge(UIDraggable other)
        {
            float wowValue =
                Random.Range(-0.6666f, 1.5f);

            float extraPower = other.Uniqueness * wowValue;
            
            float totalPower =
                Uniqueness +
                extraPower;

            float otherRatio =
                totalPower > 0f
                    ? other.Uniqueness / totalPower
                    : 0.5f;

            
            
            _image.color = Color.Lerp(
                _image.color,
                other._image.color,
                otherRatio
            );

            Uniqueness = totalPower;

            _vfxModule.PlayVfx(
                wowValue > 0f
                    ? assetNameGreen.AssetHash
                    : assetNameRed.AssetHash
            );

            TextMeshProUGUI t = Instantiate(_yesText, yesTextBundle);

            t.text = extraPower == 0 ? "0" : (int)extraPower > 0 ? $"+{(int)extraPower}" : $"{(int)extraPower}";
            
            RectTransform rect = t.rectTransform;

            float startY = rect.anchoredPosition.y;
            float duration = 1f;
            float moveDistance = 20f;

            rect.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            
            Sequence sequence = DOTween.Sequence();

            sequence.Join(
                rect.DOAnchorPosY(startY + moveDistance, duration)
                    .SetEase(Ease.OutQuad)
            );

            sequence.Join(
                t.DOFade(0f, duration)
            );

            sequence.OnComplete(() =>
            {
                Destroy(t.gameObject);
            });

            _text.text =
                ((int)Uniqueness)
                .ToString(CultureInfo.InvariantCulture);

            if (totalPower <= 0)
            {
                Destroy(gameObject);
            }
        }
        
        
        private void ClampToDragArea()
        {
            if (dragArea == null)
                return;

            dragArea.GetWorldCorners(_dragAreaCorners);
            _rectTransform.GetWorldCorners(_draggableCorners);

            Vector3 correction = Vector3.zero;

            // 왼쪽 경계
            if (_draggableCorners[0].x <
                _dragAreaCorners[0].x)
            {
                correction.x =
                    _dragAreaCorners[0].x -
                    _draggableCorners[0].x;
            }
            // 오른쪽 경계
            else if (_draggableCorners[2].x >
                     _dragAreaCorners[2].x)
            {
                correction.x =
                    _dragAreaCorners[2].x -
                    _draggableCorners[2].x;
            }

            // 아래쪽 경계
            if (_draggableCorners[0].y <
                _dragAreaCorners[0].y)
            {
                correction.y =
                    _dragAreaCorners[0].y -
                    _draggableCorners[0].y;
            }
            // 위쪽 경계
            else if (_draggableCorners[2].y >
                     _dragAreaCorners[2].y)
            {
                correction.y =
                    _dragAreaCorners[2].y -
                    _draggableCorners[2].y;
            }

            _rectTransform.position += correction;
        }

        private static bool IsOverlap(
            RectTransform first,
            RectTransform second)
        {
            Vector3[] firstCorners = new Vector3[4];
            Vector3[] secondCorners = new Vector3[4];

            first.GetWorldCorners(firstCorners);
            second.GetWorldCorners(secondCorners);

            Rect firstRect = new Rect(
                firstCorners[0].x,
                firstCorners[0].y,
                firstCorners[2].x - firstCorners[0].x,
                firstCorners[2].y - firstCorners[0].y
            );

            Rect secondRect = new Rect(
                secondCorners[0].x,
                secondCorners[0].y,
                secondCorners[2].x - secondCorners[0].x,
                secondCorners[2].y - secondCorners[0].y
            );

            return firstRect.Overlaps(secondRect);
        }

        public void New()
        {
            throw new System.NotImplementedException();
        }

        public void Free()
        {
            throw new System.NotImplementedException();
        }

        public PoolItemSO PoolItem { get; set; }

        public GameObject GameObject => gameObject;

        public void ResetItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
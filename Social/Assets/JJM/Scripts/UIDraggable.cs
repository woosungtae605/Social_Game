using System.Collections.Generic;
using DevLib.ObjectPool.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDraggable : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPoolable
{
    [field: SerializeField]
    public float PropagationPower { get; private set; } = 50f;

    private static readonly List<UIDraggable> Draggables = new();

    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>().rootCanvas;
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < Draggables.Count; i++)
        {
            UIDraggable other = Draggables[i];

            if (other == this)
                continue;

            if (!IsOverlap(_rectTransform, other._rectTransform))
                continue;
            
            other.Merge(this);

            Destroy(gameObject);
            return;
        }
    }

    private void Merge(UIDraggable other)
    {
        float totalPower =
            PropagationPower + other.PropagationPower;

        float otherRatio = totalPower > 0f
            ? other.PropagationPower / totalPower
            : 0.5f;
        
        _image.color = Color.Lerp(
            _image.color,
            other._image.color,
            otherRatio
        );
        
        PropagationPower = totalPower;

        Debug.Log(
            $"{name} 전파력: {PropagationPower}, 색상: {_image.color}"
        );
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
    public GameObject GameObject { get; }
    public void ResetItem()
    {
        throw new System.NotImplementedException();
    }
}
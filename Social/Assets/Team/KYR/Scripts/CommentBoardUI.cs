using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentBoardUI : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject boardPanel;
    [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    [Header("Comment")]
    [SerializeField] private Transform commentParent;
    [SerializeField] private CommentItemUI commentItemPrefab;
    [SerializeField] private List<CommentDataSO> comments = new List<CommentDataSO>();

    private void Awake()
    {
        boardPanel.SetActive(false);

        openButton.onClick.AddListener(OpenBoard);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseBoard);
    }

    private void OpenBoard()
    {
        boardPanel.SetActive(true);
        boardPanel.transform.SetAsLastSibling();

        ClearComments();
        SpawnComments();
    }

    private void CloseBoard()
    {
        boardPanel.SetActive(false);
    }

    private void SpawnComments()
    {
        foreach (CommentDataSO comment in comments)
        {
            CommentItemUI item = Instantiate(commentItemPrefab, commentParent);
            item.Init(comment, this);
        }
    }

    private void ClearComments()
    {
        for (int i = commentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(commentParent.GetChild(i).gameObject);
        }
    }

    public void KeepComment(CommentDataSO data)
    {
        Debug.Log($"댓글 유지: {data.content}");

        // 나중에 여기서 State 수정
        // state.ApplyComment(data);
    }

    public void DeleteComment(CommentDataSO data)
    {
        Debug.Log($"댓글 삭제: {data.content}");
    }
}
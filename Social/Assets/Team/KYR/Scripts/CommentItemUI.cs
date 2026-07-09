using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private Button keepButton;
    [SerializeField] private Button deleteButton;

    private CommentDataSO _data;
    private CommentBoardUI _boardUI;

    public void Init(CommentDataSO data, CommentBoardUI boardUI)
    {
        _data = data;
        _boardUI = boardUI;

        contentText.text = data.content;

        keepButton.onClick.RemoveAllListeners();
        deleteButton.onClick.RemoveAllListeners();

        keepButton.onClick.AddListener(Keep);
        deleteButton.onClick.AddListener(Delete);
    }

    private void Keep()
    {
        _boardUI.KeepComment(_data);
        Destroy(gameObject);
    }

    private void Delete()
    {
        _boardUI.DeleteComment(_data);
        Destroy(gameObject);
    }
}
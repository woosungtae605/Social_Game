using System;
using UnityEngine;

[Serializable]
public class BoardPostData
{
    [SerializeField] private BoardPostSO definition;
    [SerializeField] private int viewCount;
    [SerializeField] private long createdAtTicks;
    [SerializeField] private bool isConcept;

    public BoardPostSO Definition => definition;
    public string Writer => definition.Writer;
    public string Title => definition.Title;
    public int ViewCount => viewCount;
    public DateTime CreatedAt => new DateTime(createdAtTicks);
    public bool IsConcept => isConcept;

    public BoardPostData(BoardPostSO definition, DateTime createdAt)
    {
        this.definition = definition;
        viewCount = definition.InitialViewCount;
        createdAtTicks = createdAt.Ticks;
        isConcept = false;
    }

    public void ToggleConcept()
    {
        isConcept = !isConcept;
    }

    public void IncreaseViewCount()
    {
        viewCount++;
    }
}
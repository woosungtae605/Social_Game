using System;
using UnityEngine;

namespace Team.KYR.Scripts
{
    [Serializable]
    public class BoardPostData
    {
        [SerializeField] private BoardSo board;
        [SerializeField] private BoardPostSo definition;
        [SerializeField] private int viewCount;
        [SerializeField] private long createdAtTicks;
        [SerializeField] private bool isConcept;

        public BoardSo Board => board;
        public BoardPostSo Definition => definition;
        public string Writer => definition.Writer;
        public string Title => definition.Title;
        public int ViewCount => viewCount;
        public DateTime CreatedAt => new DateTime(createdAtTicks);
        public bool IsConcept => isConcept;

        public BoardPostData(BoardSo board, BoardPostSo definition, DateTime createdAt)
        {
            this.board = board;
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
}

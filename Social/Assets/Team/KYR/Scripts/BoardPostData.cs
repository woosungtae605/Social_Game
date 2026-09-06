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
        [SerializeField] private int recommendCount;
        [SerializeField] private int dislikeCount;
        [SerializeField] private float spawnedAt;

        public BoardSo Board => board;
        public BoardPostSo Definition => definition;
        public string Writer => definition.Writer;
        public string Title => definition.Title;
        public int ViewCount => viewCount;
        public DateTime CreatedAt => new DateTime(createdAtTicks);
        public bool IsConcept => isConcept;
        public int RecommendCount => recommendCount;
        public int DislikeCount => dislikeCount;
        public bool IsHarmful => definition != null && definition.IsHarmful;
        public float SpawnedAt => spawnedAt;

        public string Body
        {
            get
            {
                if (definition == null || definition.Content == null)
                    return string.Empty;

                return definition.Content.Body;
            }
        }

        public Sprite[] Images
        {
            get
            {
                if (definition == null || definition.Content == null)
                    return new Sprite[0];

                return definition.Content.Images;
            }
        }

        public BoardPostData(BoardSo board, BoardPostSo definition, DateTime createdAt, float spawnedAt)
        {
            this.board = board;
            this.definition = definition;
            viewCount = definition.InitialViewCount;
            createdAtTicks = createdAt.Ticks;
            isConcept = false;
            recommendCount = 0;
            dislikeCount = 0;
            this.spawnedAt = spawnedAt;
        }

        public void VoteRecommend()
        {
            if (recommendCount == 1)
                return;

            recommendCount = 1;
            dislikeCount = 0;
            isConcept = true;
        }

        public void VoteDislike()
        {
            if (dislikeCount == 1)
                return;

            dislikeCount = 1;
            recommendCount = 0;
            isConcept = false;
        }

        public void ToggleConcept()
        {
            isConcept = !isConcept;
            recommendCount = isConcept ? 1 : 0;

            if (isConcept)
                dislikeCount = 0;
        }

        public void IncreaseViewCount()
        {
            viewCount++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Team.KYR.Scripts;
using UnityEngine;

namespace Team.WST.Scripts.CulturalScabs
{
    public class CulturalScabSpawner : MonoBehaviour
    {
        [SerializeField] private CulturalScabManager manager;
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private BoardSo targetBoard;
        [SerializeField] private int initialCount = 8;
        [SerializeField] private int maxAlive = 12;
        [SerializeField] private float minUniqueness = 50f;
        [SerializeField] private float maxUniqueness = 50f;
        [SerializeField] private float minInterval = 20f;
        [SerializeField] private float maxInterval = 35f;

        private readonly List<BoardPostData> sharePosts = new List<BoardPostData>();

        private void Start()
        {
            if (boardManager != null)
                boardManager.EnsureInitialized();

            int spawnCount = Mathf.Max(0, initialCount);
            for (int i = 0; i < spawnCount; i++)
                TrySpawnOne();

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (enabled)
            {
                float wait = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(wait);
                TrySpawnOne();
            }
        }

        private void TrySpawnOne()
        {
            if (manager == null)
                return;

            if (manager.Scabs.Count >= maxAlive)
                return;

            BoardPostData post = PickSharePost();
            if (post == null)
                return;

            float uniqueness = Random.Range(minUniqueness, maxUniqueness);
            manager.Spawn(uniqueness, SharePostCultureRecipe.Build(post.OriginCountry, post.Kind));
        }

        private BoardPostData PickSharePost()
        {
            CollectSharePosts();
            if (sharePosts.Count == 0)
                return null;

            return sharePosts[Random.Range(0, sharePosts.Count)];
        }

        private void CollectSharePosts()
        {
            sharePosts.Clear();
            if (boardManager == null || targetBoard == null)
                return;

            boardManager.EnsureInitialized();
            IReadOnlyList<BoardPostData> posts = boardManager.Posts;
            for (int i = 0; i < posts.Count; i++)
            {
                BoardPostData post = posts[i];
                if (post == null || post.Board != targetBoard || post.Definition == null)
                    continue;

                sharePosts.Add(post);
            }
        }
    }
}

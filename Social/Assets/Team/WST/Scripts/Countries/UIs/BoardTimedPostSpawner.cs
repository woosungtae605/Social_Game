using System.Collections;
using Team.KYR.Scripts;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class BoardTimedPostSpawner : MonoBehaviour
    {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private BoardSo board;
        [SerializeField] private BoardPostSo[] postPool;
        [SerializeField] private float minInterval = 20f;
        [SerializeField] private float maxInterval = 35f;

        private void Start()
        {
            if (boardManager != null)
                boardManager.EnsureInitialized();

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (enabled)
            {
                float wait = Random.Range(minInterval, maxInterval);
                yield return new WaitForSeconds(wait);
                SpawnOne();
            }
        }

        private void SpawnOne()
        {
            if (boardManager == null || board == null || postPool == null || postPool.Length == 0)
                return;

            BoardPostSo chosen = postPool[Random.Range(0, postPool.Length)];
            if (chosen == null)
                return;

            boardManager.SpawnPost(board, chosen);
        }
    }
}

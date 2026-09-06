using Team.KYR.Scripts;
using UnityEngine;

namespace Team.WST.Scripts.Countries.UIs
{
    public class BoardPostExpirySystem : MonoBehaviour
    {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private float lifetimeSeconds = 300f;
        [SerializeField] private float checkInterval = 1f;

        private float checkTimer;

        private void Start()
        {
            if (boardManager != null)
                boardManager.EnsureInitialized();
        }

        private void Update()
        {
            if (boardManager == null || lifetimeSeconds <= 0f)
                return;

            checkTimer += Time.deltaTime;
            if (checkTimer < checkInterval)
                return;

            checkTimer = 0f;
            boardManager.RemoveExpiredPosts(lifetimeSeconds);
        }
    }
}

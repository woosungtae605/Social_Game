using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Match
{
    public class MatchCountdown : MonoBehaviour
    {
        [SerializeField] private float durationSeconds = 900f;

        private float remainingSeconds;
        private bool expired;

        private void Awake()
        {
            MatchOutcome.Reset();
            remainingSeconds = Mathf.Max(0f, durationSeconds);
            expired = false;
            Bus<MatchEndedEvent>.OnEvent += HandleMatchEnded;
        }

        private void Start()
        {
            RaiseTick();
        }

        private void OnDestroy()
        {
            Bus<MatchEndedEvent>.OnEvent -= HandleMatchEnded;
        }

        private void Update()
        {
            if (expired || MatchOutcome.HasEnded)
                return;

            remainingSeconds -= Time.deltaTime;
            if (remainingSeconds > 0f)
            {
                RaiseTick();
                return;
            }

            remainingSeconds = 0f;
            expired = true;
            RaiseTick();
            Bus<MatchTimeExpiredEvent>.RaiseEvent(new MatchTimeExpiredEvent());
        }

        private void HandleMatchEnded(MatchEndedEvent evt)
        {
            expired = true;
        }

        private void RaiseTick()
        {
            Bus<MatchCountdownTickEvent>.RaiseEvent(new MatchCountdownTickEvent(remainingSeconds));
        }
    }
}

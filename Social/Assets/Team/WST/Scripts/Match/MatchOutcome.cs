using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using UnityEngine;

namespace Team.WST.Scripts.Match
{
    public static class MatchOutcome
    {
        public static bool HasEnded { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            HasEnded = false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetBeforeSceneLoad()
        {
            HasEnded = false;
            Time.timeScale = 1f;
        }

        public static void Reset()
        {
            HasEnded = false;
            Time.timeScale = 1f;
        }

        public static bool TryEnd(MatchResult result)
        {
            if (HasEnded)
                return false;

            HasEnded = true;
            Bus<MatchEndedEvent>.RaiseEvent(new MatchEndedEvent(result));
            return true;
        }
    }
}

using Team.WST.Scripts.CoreSystem;

namespace Team.WST.Scripts.Events
{
    public struct MatchCountdownTickEvent : IEvent
    {
        public readonly float RemainingSeconds;

        public MatchCountdownTickEvent(float remainingSeconds)
        {
            RemainingSeconds = remainingSeconds;
        }
    }
}

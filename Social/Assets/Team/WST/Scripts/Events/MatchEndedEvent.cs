using Team.WST.Scripts.CoreSystem;

namespace Team.WST.Scripts.Events
{
    public enum MatchResult
    {
        Cleared,
        Failed
    }

    public struct MatchEndedEvent : IEvent
    {
        public readonly MatchResult Result;

        public MatchEndedEvent(MatchResult result)
        {
            Result = result;
        }
    }
}

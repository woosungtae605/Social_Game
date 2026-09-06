using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.CulturalScabs;

namespace Team.WST.Scripts.Events
{
    public struct CulturalScabSpawnedEvent : IEvent
    {
        public readonly CulturalScab Scab;

        public CulturalScabSpawnedEvent(CulturalScab scab)
        {
            Scab = scab;
        }
    }

    public struct CulturalScabDespawnedEvent : IEvent
    {
        public readonly CulturalScab Scab;

        public CulturalScabDespawnedEvent(CulturalScab scab)
        {
            Scab = scab;
        }
    }

    public struct CulturalScabHoveredEvent : IEvent
    {
        public readonly CulturalScab Scab;

        public CulturalScabHoveredEvent(CulturalScab scab)
        {
            Scab = scab;
        }
    }

    public struct CulturalScabUnhoveredEvent : IEvent
    {
        public readonly CulturalScab Scab;

        public CulturalScabUnhoveredEvent(CulturalScab scab)
        {
            Scab = scab;
        }
    }
}

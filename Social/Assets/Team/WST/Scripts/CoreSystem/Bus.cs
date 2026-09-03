using System;

namespace Team.WST.Scripts.CoreSystem
{
    public static class Bus<T> where T : IEvent
    {
        public static event Action<T> OnEvent;
        public static void RaiseEvent(T evnt) => OnEvent?.Invoke(evnt);
    }
}
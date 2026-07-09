using System;

namespace Team.WST.Scripts.CoreSystem
{
    public class Bus<T> where T : IEvent
    {
        public Action<T> OnEvnent;
        public void RaiseEvent(T evnt) => OnEvnent?.Invoke(evnt);
    }
}
using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    public abstract class HistoryEventSO : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Content { get; private set; }

        public abstract void Apply(AbstractCountry country);
        public abstract void Revert(AbstractCountry country);
        public abstract bool CanApply(AbstractCountry country);
    }
}
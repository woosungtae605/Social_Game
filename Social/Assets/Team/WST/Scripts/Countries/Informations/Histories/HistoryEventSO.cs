using UnityEngine;

namespace Team.WST.Scripts.Countries.Informations.Histories
{
    public abstract class HistoryEventSO : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Content { get; private set; }

        public abstract void Apply(AbstractCountry abstractCountry);
        public abstract bool CanApply(AbstractCountry abstractCountry);
    }
}
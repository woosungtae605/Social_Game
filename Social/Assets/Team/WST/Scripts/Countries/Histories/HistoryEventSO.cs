using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    public abstract class HistoryEventSO : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Content { get; private set; }

        public virtual bool IsActive => false;

        public abstract void Apply(AbstractCountry country);
        public abstract void Revert(AbstractCountry country);
        public abstract bool CanApply(AbstractCountry country);

        protected bool IsAlreadyActiveOn(AbstractCountry country)
        {
            foreach (HistoryEventSO history in country.CultureHistory)
            {
                if (history != null && history.GetType() == GetType() && history.IsActive)
                    return true;
            }

            return false;
        }
    }
}
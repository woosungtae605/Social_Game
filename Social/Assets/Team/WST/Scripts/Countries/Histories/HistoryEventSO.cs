using UnityEngine;

namespace Team.WST.Scripts.Countries.Histories
{
    public abstract class HistoryEventSO : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Content { get; private set; }

        public abstract void Apply();
        public abstract void Revert();
        public abstract bool CanApply();
    }
}
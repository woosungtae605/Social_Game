using UnityEngine;

namespace Team.WST.Scripts.Countries.Informations
{
    [CreateAssetMenu(fileName = "Culture", menuName = "SO/Culture", order = 1)]
    public class CultureSO : ScriptableObject
    {
        [field: SerializeField] public string CultureName { get; private set; }
        [field: SerializeField, TextArea(2, 6)] public string Description { get; private set; }
        [field: SerializeField] public CountryType OriginCountry { get; private set; }
        [field: SerializeField] public int Influence { get; private set; }
        [field: SerializeField, Range(0, 100)] public int Harmfulness { get; private set; }

        public bool ShouldKeepOnMerge => Harmfulness <= 0;

        public int HarmfulnessToCarryOnMerge => ShouldKeepOnMerge ? 0 : Harmfulness;
    }
}

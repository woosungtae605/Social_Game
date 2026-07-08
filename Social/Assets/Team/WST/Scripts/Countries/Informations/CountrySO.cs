using UnityEngine;

namespace Team.WST.Scripts.Countries.Informations
{
    [CreateAssetMenu(fileName = "Country", menuName = "SO/Country", order = 0)]
    public class CountrySO : ScriptableObject
    {
        [field: SerializeField] public CountryType CountryType { get; private set; }
        [field: SerializeField] public string CountryName { get; private set; }
        [field: SerializeField] public Sprite CountrySprite { get; private set; }
        [field: SerializeField] public int InitCulturalPower { get; private set; }
        [field: SerializeField] public Color CountryColor { get; private set; }
    }
}
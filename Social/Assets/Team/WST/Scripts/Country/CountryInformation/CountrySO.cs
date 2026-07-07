using UnityEngine;

namespace Team.WST.Scripts.Country.CountryInformation
{
    [CreateAssetMenu(fileName = "Country", menuName = "SO/Country", order = 0)]
    public class CountrySO : ScriptableObject
    {
        [field: SerializeField] public string CountryName { get; private set; }
    }
}
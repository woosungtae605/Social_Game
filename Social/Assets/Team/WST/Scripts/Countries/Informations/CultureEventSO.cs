using UnityEngine;

namespace Team.WST.Scripts.Countries.Informations
{
    [CreateAssetMenu(fileName = "CultureEvent", menuName = "SO/CultureEvent", order = 0)]
    public class CultureEventSO : ScriptableObject
    {
        [field: SerializeField] public string Title { get; private set; }
        [field: SerializeField, TextArea] public string Content { get; private set; }
        [field: SerializeField] public int Amount { get; private set; }
    }
}
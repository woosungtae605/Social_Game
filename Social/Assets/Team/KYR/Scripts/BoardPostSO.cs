using Team.WST.Scripts.Countries.Informations;
using UnityEngine;

namespace Team.KYR.Scripts
{
    [CreateAssetMenu(fileName = "BoardPost", menuName = "Board/Board Post")]
    public class BoardPostSo : ScriptableObject
    {
        [SerializeField] private string writer;
        [SerializeField] private string title;
        [SerializeField] private int initialViewCount;
        [SerializeField] private int kind = 100;
        [SerializeField] private CountryType originCountry = CountryType.KOREA;
        [SerializeField] private BoardPostContentSo content;

        public const int GoodKind = 100;
        public const int HarmfulKindBelow = 50;

        public string Writer => writer;
        public string Title => title;
        public int InitialViewCount => initialViewCount;
        public int Kind => kind;
        public bool IsGood => kind == GoodKind;
        public bool IsHarmful => kind < HarmfulKindBelow;
        public CountryType OriginCountry => originCountry;
        public BoardPostContentSo Content => content;

        private void OnValidate()
        {
            if (initialViewCount < 0)
                initialViewCount = 0;

            if (kind < 0)
                kind = 0;
        }
    }
}
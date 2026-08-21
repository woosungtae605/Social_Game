using UnityEngine;

namespace Team.KYR.Scripts
{
    [CreateAssetMenu(fileName = "BoardPost", menuName = "Board/Board Post")]
    public class BoardPostSo : ScriptableObject
    {
        [SerializeField] private string writer;
        [SerializeField] private string title;
        [SerializeField] private int initialViewCount;

        public string Writer => writer;
        public string Title => title;
        public int InitialViewCount => initialViewCount;

        private void OnValidate()
        {
            if (initialViewCount < 0)
                initialViewCount = 0;
        }
    }
}
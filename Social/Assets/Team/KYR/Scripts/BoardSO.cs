using UnityEngine;

namespace Team.KYR.Scripts
{
    [CreateAssetMenu(fileName = "Board", menuName = "Board/Board")]
    public class BoardSo : ScriptableObject
    {
        [SerializeField] private string displayName;
        [SerializeField] private Sprite icon;
        [SerializeField] private BoardPostSo[] initialPosts;

        public string DisplayName => displayName;
        public Sprite Icon => icon;

        public BoardPostSo[] InitialPosts
        {
            get
            {
                if (initialPosts == null)
                    return new BoardPostSo[0];

                return initialPosts;
            }
        }
    }
}

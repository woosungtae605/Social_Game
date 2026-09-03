using UnityEngine;

namespace Team.KYR.Scripts
{
    [CreateAssetMenu(fileName = "BoardPostContent", menuName = "Board/Board Post Content")]
    public class BoardPostContentSo : ScriptableObject
    {
        [SerializeField, TextArea(5, 20)] private string body;
        [SerializeField] private Sprite[] images;

        public string Body
        {
            get
            {
                if (body == null)
                    return string.Empty;

                return body;
            }
        }

        public Sprite[] Images
        {
            get
            {
                if (images == null)
                    return new Sprite[0];

                return images;
            }
        }
    }
}

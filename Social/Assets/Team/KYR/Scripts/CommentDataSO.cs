using UnityEngine;
                
              
                
                [CreateAssetMenu(fileName = "CommentData", menuName = "SO/Comment Data")]
                public class CommentDataSO : ScriptableObject
                {
                    public string userName;
                    [TextArea]
                    public string content;
                    
                    
                    public int identityChange;
                    public int receptivityChange;
                    public int popularityChange;
                    public int mixChange;
                }
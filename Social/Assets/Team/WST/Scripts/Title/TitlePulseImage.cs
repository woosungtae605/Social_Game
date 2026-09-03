using UnityEngine;
using UnityEngine.UI;

namespace Team.WST.Scripts.Title
{
    public class TitlePulseImage : MonoBehaviour
    {
        [SerializeField] private Image target;
        [SerializeField] private float minAlpha = 0.25f;
        [SerializeField] private float maxAlpha = 0.9f;
        [SerializeField] private float speed = 1.4f;
        [SerializeField] private float phase;

        private void Awake()
        {
            if (target == null)
                target = GetComponent<Image>();
        }

        private void Update()
        {
            if (target == null)
                return;

            float t = (Mathf.Sin((Time.time + phase) * speed) + 1f) * 0.5f;
            Color color = target.color;
            color.a = Mathf.Lerp(minAlpha, maxAlpha, t);
            target.color = color;
        }
    }
}

using Team.WST.Scripts.CoreSystem;
using Team.WST.Scripts.Events;
using TMPro;
using UnityEngine;

namespace Team.WST.Scripts.Match
{
    public class MatchCountdownView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;

        private int lastShownSeconds = int.MinValue;

        private void OnEnable()
        {
            Bus<MatchCountdownTickEvent>.OnEvent += HandleTick;
        }

        private void OnDisable()
        {
            Bus<MatchCountdownTickEvent>.OnEvent -= HandleTick;
        }

        private void HandleTick(MatchCountdownTickEvent evt)
        {
            int seconds = Mathf.Max(0, Mathf.CeilToInt(evt.RemainingSeconds));
            if (seconds == lastShownSeconds)
                return;

            lastShownSeconds = seconds;
            if (timeText == null)
                return;

            int minutes = seconds / 60;
            int remain = seconds % 60;
            timeText.text = minutes.ToString("00") + ":" + remain.ToString("00");
        }
    }
}

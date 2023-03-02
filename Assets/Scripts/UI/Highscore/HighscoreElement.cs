using System;
using TMPro;
using UnityEngine;

namespace UI.Highscore
{
    [DisallowMultipleComponent()]
    public class HighscoreElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timeText;

        public void Initialize(Payload payload)
        {
            scoreText.text = $"{payload.Score:0.00}";
            timeText.text = $"{payload.Time}";
        }

        public class Payload
        {
            public Payload(float score, string time)
            {
                Score = score;

                if (DateTime.TryParse(time, out var date))
                {
                    Time = date;
                }
                else Debug.LogError($"Cannot parse time: {time} to DateTime");
            }

            public float Score { get; private set; }
            public DateTime Time { get; private set; }
        }
    }
}

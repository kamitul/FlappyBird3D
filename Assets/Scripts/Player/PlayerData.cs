using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = "Player/Data")]
    public class PlayerData : ScriptableObject
    {
        public float Score { get; set; } = 0f;

        public void Update()
        {
            Score += Time.deltaTime;
        }

        public void Reset()
        {
            Score = 0f;
        }
    }
}

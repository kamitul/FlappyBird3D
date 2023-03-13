using UnityEngine;

namespace Player
{
    public struct PlayerData
    {
        public float Score;

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

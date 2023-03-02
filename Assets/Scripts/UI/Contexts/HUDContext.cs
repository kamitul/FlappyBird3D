using Player;
using System;
using TMPro;
using UnityEngine;

namespace UI.Contexts
{
    public class HUDContext : Context, IObserver<PlayerData>
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        public override ContextIdentifier Identifier => ContextIdentifier.HUD;

        public void OnCompleted()
        {
            Debug.Log("Completed");
        }

        public void OnError(Exception error)
        {
            Debug.LogError($"{error.Message}");
        }

        public void OnNext(PlayerData value)
        {
            scoreText.text = $"Score: {value.Score:0.00}";
        }
    }
}

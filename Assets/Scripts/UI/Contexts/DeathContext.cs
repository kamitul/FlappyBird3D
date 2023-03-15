using Config;
using Cysharp.Threading.Tasks;
using Player;
using Logic;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Services.Saves;

namespace UI.Contexts
{
    public class DeathContext : Context, IObserver<PlayerData>
    {
        [SerializeField] private Button exitMenuButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI buttonText;

        private PlayerData data;

        public override ContextIdentifier Identifier => ContextIdentifier.Death;

        private void OnEnable()
        {
            exitMenuButton.onClick.AddListener(OnMenuOpened);
        }

        private void OnDisable()
        {
            exitMenuButton.onClick.RemoveListener(OnMenuOpened);
        }

        public override async void Open()
        {
            base.Open();
            scoreText.text = $"Your score: {data.Score:0.00}";
            exitMenuButton.interactable = false;
            buttonText.text = "Saving...";
            await SaveScore();
            exitMenuButton.interactable = true;
            buttonText.text = "Exit";
        }

        private async UniTask SaveScore()
        {
            var ct = CTS.Token;
            var scores = await GameServices.GetService<SaveService>().GetValue<List<SaveEntity>>(SaveConfig.SCORE_KEY, ct);
            if (scores == null) scores = new List<SaveEntity>();
            if(scores.Count > SaveConfig.MAX_SAVED_SCORES)
            {
                var last = scores.Last();
                if (last != null && last.Score >= data.Score) return; 
            }

            scores.Add(new SaveEntity(DateTime.UtcNow.ToString(), data.Score));
            await GameServices.GetService<SaveService>().SetValue(SaveConfig.SCORE_KEY, 
                scores.OrderByDescending(x => x.Score).Take(SaveConfig.MAX_SAVED_SCORES).ToList(), ct);
        }

        private void OnMenuOpened()
        {
            controller.Open(ContextIdentifier.Menu);
        }

        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(PlayerData value)
        {
            data = value;
        }
    }
}

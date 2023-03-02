using Config;
using Logic;
using Services.Models;
using Services.Save;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Highscore;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Contexts
{
    public class HighscoreContext : Context
    {
        [SerializeField] private Button backButton;
        [SerializeField] private RectTransform layoutParent;
        [SerializeField] private HighscoreElement elementPrefab;

        private readonly List<HighscoreElement> spawnedElements = new List<HighscoreElement>();

        public override ContextIdentifier Identifier => ContextIdentifier.Highscore;

        private void OnEnable()
        {
            backButton.onClick.AddListener(OnBack);
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveListener(OnBack);
        }

        private void OnBack()
        {
            controller.Open(ContextIdentifier.Menu);
        }

        public override async void Open()
        {
            base.Open();
            ClearLayout();
            await GetHighscore();
        }

        private async Task GetHighscore()
        {
            var ct = CTS.Token;
            var res = await GameServices.GetService<SaveService>().GetValue<List<SaveEntity>>(SaveConfig.SCORE_KEY, ct);
            if (res == null)
            {
                Debug.LogError("Error downloading saves!");
                return;
            }

            if (ct.IsCancellationRequested)
            {
                Debug.LogWarning("Cancellation requested!");
                return;
            }

            SpawnLayout(res);
        }

        private void ClearLayout()
        {
            if(spawnedElements.Count > 0)
            {
                spawnedElements.ForEach(x => Destroy(x.gameObject));
                spawnedElements.Clear();
            }
        }

        private void SpawnLayout(List<SaveEntity> saves)
        {
            for (int i = 0; i < saves.Count; ++i)
            {
                var obj = Instantiate(elementPrefab, layoutParent);
                obj.Initialize(new HighscoreElement.Payload(saves[i].Score, saves[i].Date));
                spawnedElements.Add(obj);
            }
        }
    }
}
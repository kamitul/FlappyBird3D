using Cysharp.Threading.Tasks;
using Logic;
using TMPro;
using UnityEngine;

namespace UI.Contexts
{
    public class CountdownContext : Context
    {
        private const int COUNTDOWN_TIMER = 3;
        private const int COUNTDOWN_OFFSET_MS = 1000;

        [SerializeField] private TextMeshProUGUI timerText;

        public override ContextIdentifier Identifier => ContextIdentifier.Countdown;

        public override async void Open()
        {
            base.Open();
            GameManager.Instance.Prepare();
            await Delay();
            controller.Open(ContextIdentifier.HUD);
            GameManager.Instance.Begin();
        }

        public async UniTask Delay()
        {
            for(int i = 0; i< COUNTDOWN_TIMER; ++i)
            {
                timerText.text = $"{COUNTDOWN_TIMER - i}";
                await UniTask.Delay(COUNTDOWN_OFFSET_MS);
            }
        }
    }
}
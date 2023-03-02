using Logic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Contexts
{
    public class MenuContext : Context
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button highscoreButton;
        [SerializeField] private Button exitButton;

        public override ContextIdentifier Identifier => ContextIdentifier.Menu;

        private void OnEnable()
        {
            startButton.onClick.AddListener(OnGameStarted);
            highscoreButton.onClick.AddListener(OnHighscoreOpened);
            exitButton.onClick.AddListener(OnGameExited);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveListener(OnGameStarted);
            highscoreButton.onClick.RemoveListener(OnHighscoreOpened);
            exitButton.onClick.RemoveListener(OnGameExited);
        }

        private void OnGameStarted()
        {
            controller.Open(ContextIdentifier.Countdown);
        }

        private void OnHighscoreOpened()
        {
            controller.Open(ContextIdentifier.Highscore);
        }

        private void OnGameExited()
        {
            GameManager.Instance.Quit();
        }
    }
}
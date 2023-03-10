using Config;
using Map;
using Player;
using Spawner;
using System;
using UI;
using UI.Contexts;
using UnityEngine;
using Utils;

namespace Logic
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private MapController mapController;
        [SerializeField] private UIController uiController;

        private PlayerController playerController;
        private PointsController pointsController;
        private TileController tileController;

        private IDisposable[] disposables;

        protected override void Awake()
        {
            base.Awake();
            pointsController = new PointsController();
            uiController.Initialize();

            disposables = new IDisposable[2];
            disposables[0] = pointsController.Subscribe(uiController.GetHUD());
            disposables[0] = pointsController.Subscribe(uiController.GetDeathContext());
        }

        public void InitializeUI()
        {
            uiController.Open(ContextIdentifier.Menu);
        }

        private void Start()
        {
            var prefabsConfig = Configuration.GetConfig<PrefabsConfig>();
            playerController = Instantiate(prefabsConfig.PlayerPrefab);
            playerController.Disable();
            mapController.Subscribe(new AcceleratorController(playerController));
            mapController.Initialize();
            tileController = new TileController(new TileController.Payload(playerController, mapController));
        }

        public void Prepare()
        {
            mapController.Reset();
            playerController.Enable();
            playerController.SetState(State.Start);
        }

        public void Begin()
        {
            playerController.OnPlayerDeath += OnPlayerDeath;
            playerController.OnPlayerFly += OnPlayerFly;
            playerController.SetState(State.Flying);
        }

        private void OnPlayerFly()
        {
            pointsController.Begin();
        }

        private void OnPlayerDeath()
        {
            pointsController.Reset();
            pointsController.Stop();
            uiController.Open(ContextIdentifier.Death);
        }

        private void OnDisable()
        {
            if (playerController != null)
            {
                playerController.OnPlayerDeath -= OnPlayerDeath;
                playerController.OnPlayerFly -= OnPlayerFly;
            }

            foreach(var disposable in disposables)
                disposable?.Dispose();
        }

        private void Update()
        {
            if (pointsController != null)
                pointsController.Update();
            if(tileController != null)
                tileController.Update();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
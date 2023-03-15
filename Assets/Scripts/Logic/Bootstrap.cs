using Config;
using Constants;
using Cysharp.Threading.Tasks;
using Loader;
using Services.Assets;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Logic
{
    [DisallowMultipleComponent()]
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private BootstrapUI bootstrapUI;

        private LoaderController controller;
        private IDisposable disposable;

        private void Awake()
        {
            controller = new LoaderController();
            disposable = controller.Subscribe(bootstrapUI);
            Application.targetFrameRate = Constant.TARGET_FRAMERATE;
        }

        private async void Start()
        {
            await Initialize();

            var operation = SceneManager.LoadSceneAsync(Constant.SCENE_GAME_NAME, 
                LoadSceneMode.Additive);
            operation.completed += (_) => OnLoadedCompleted(Constant.SCENE_GAME_NAME);
        }

        private void OnLoadedCompleted(string name)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
            GameManager.Instance.InitializeUI();
        }

        private async UniTask Initialize()
        {
            await Addressables.InitializeAsync().Task;
            controller.UpdateProgress(0.35f);

            await Configuration.Load();
            controller.UpdateProgress(0.65f);

            //REMOVE LATER
            await UniTask.Delay(1500);

            await GameServices.GetService<OnlineAssetService>().Download();
            controller.UpdateProgress(1f);

            bootstrapUI.Close();
            disposable?.Dispose();

            Destroy(gameObject);
        }
    }
}

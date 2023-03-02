using Constants;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Config
{
    public static class Configuration
    {
        private static List<ScriptableObject> cfgs = new List<ScriptableObject>();

        public static async UniTask Load()
        {
            var handle = Addressables.LoadResourceLocationsAsync(Constant.ADDRESSABLE_CONFIG_LABEL, typeof(ScriptableObject));

            try
            {
                var locations = await handle.Task.AsUniTask();
                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError($"Failed getting config files!");
                    return;
                }

                var res = await UniTask.WhenAll(locations.Select(x => Addressables.LoadAssetAsync<ScriptableObject>(x).Task.AsUniTask()));
                cfgs = res.ToList();
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static T GetConfig<T>() where T : ScriptableObject
        {
            var foundConfig = cfgs.Find(c => c is T) as T;
            if (foundConfig) return foundConfig;
            else
            {
                Debug.LogError($"Could not find config {typeof(T).Name}");
                return null;
            }
        }
    }
}
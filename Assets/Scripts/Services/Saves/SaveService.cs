using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using UnityEngine;
using static Config.SaveConfig;

namespace Services.Save
{
    public class SaveService : IService
    {
        private readonly string SAVE_DIR_PATH = Application.persistentDataPath;

        public async UniTask<T> GetValue<T>(SaveKey key, CancellationToken ct = default)
        {
            var filePath = Path.Combine(SAVE_DIR_PATH, $"{key.Key}.json");
            if (!File.Exists(filePath))
            {
                Debug.LogError($"File:{filePath} doesn't exist!");
                return default;
            }

            try
            {
                var res = await File.ReadAllTextAsync(filePath, ct);
                return JsonConvert.DeserializeObject<T>(res);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return default;
            }
        }

        public async UniTask SetValue<T>(SaveKey key, T data, CancellationToken ct = default)
        {
            var filePath = Path.Combine(SAVE_DIR_PATH, $"{key.Key}.json");
            try
            {
                var json = JsonConvert.SerializeObject(data);
                await File.WriteAllTextAsync(filePath, json, ct);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
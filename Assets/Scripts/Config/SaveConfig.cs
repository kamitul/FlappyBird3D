using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(SaveConfig), menuName = "Save/Config")]
    public class SaveConfig : ScriptableObject
    {
        public const int MAX_SAVED_SCORES = 20;
        public static SaveKey SCORE_KEY => new SaveKey("HIGHSCORES_KEY");

        public class SaveKey
        {
            public SaveKey(string key)
            {
                Key = key;
            }

            public string Key { get; private set; }
        }
    }
}

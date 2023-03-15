using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(MapConfig), menuName = "Map/Config")]
    public sealed class MapConfig : ScriptableObject
    {
        [field: SerializeField] public int MAX_VISIBILE_TILES { get; private set; } = 5;
        [field: SerializeField] public int OBSTACLES_PASSED_ACCELERATOR { get; private set; } = 20;
        [field: SerializeField] public int MAX_OBSTACLES { get; private set; } = 2;
        [field: SerializeField] public float[] ZLines { get; private set; }
    }
}

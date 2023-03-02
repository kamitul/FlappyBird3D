using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(MapConfig), menuName = "Map/Config")]
    public class MapConfig : ScriptableObject
    {
        [SerializeField] public int MAX_VISIBILE_TILES = 5;
        [SerializeField] public int OBSTACLES_PASSED_ACCELERATOR = 20;
        [SerializeField] public int MAX_OBSTACLES = 2;
        [SerializeField] public float[] ZLines;
    }
}

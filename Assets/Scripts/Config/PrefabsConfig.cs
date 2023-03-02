using Player;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(PrefabsConfig), menuName = "Prefabs/Config")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public PlayerController PlayerPrefab { get; private set; }
    }
}

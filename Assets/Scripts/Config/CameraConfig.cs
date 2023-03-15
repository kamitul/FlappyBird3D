using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(CameraConfig), menuName = "Camera/Config")]
    public sealed class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 Position { get; private set; } = new Vector3(0, 1.5f, -2.5f);
        [field: SerializeField] public Vector3 Rotation { get; private set; } = new Vector3(5f, 0f, 0);
    }
}

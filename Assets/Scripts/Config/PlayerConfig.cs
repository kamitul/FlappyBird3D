using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Player/Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float HORIZONTAL_MOVEMENT_BOUNDS { get; private set; } = 3.8f;
        [field: SerializeField] public float VERTICAL_MOVEMENT_BOUNDS { get; private set; } = 3f;
        [field: SerializeField] public int StartLaneIndex { get; private set; }
        [field: SerializeField] public Vector3 BeginPosition { get; private set; } = new Vector3(-6, 1, 0f);
        [field: SerializeField] public Vector3 BeginEulerRotation { get; private set; } = new Vector3(0, 90f, 0);
        [field: SerializeField] [field: Range(0f, 10f)] public float JumpForce { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float HorizontalSpeed { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float ForwardSpeed { get; private set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float Accelerator { get; private set; }
    }
}

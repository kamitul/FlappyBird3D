using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Player/Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] public float HORIZONTAL_MOVEMENT_BOUNDS = 3.8f;
        [SerializeField] public float VERTICAL_MOVEMENT_BOUNDS = 3f;
        [field: SerializeField] public int StartLaneIndex { get; set; }

        [field: SerializeField] public Vector3 BeginPosition { get; private set; } = new Vector3(-6, 1, 0f);
        [field: SerializeField] public Vector3 BeginEulerRotation { get; private set; } = new Vector3(0, 90f, 0);
        [field: SerializeField] [field: Range(0f, 10f)] public float JumpForce { get; set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float HorizontalSpeed { get; set; }

        [field: SerializeField] [field: Range(0f, 10f)] public float ForwardSpeed { get; set; }
        [field: SerializeField] [field: Range(0f, 10f)] public float Accelerator { get; set; }
    }
}

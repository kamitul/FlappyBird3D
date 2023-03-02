using Config;
using FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public enum State
    {
        Start,
        Flying,
        Dead
    }

    [DisallowMultipleComponent()]
    [RequireComponent(typeof(PlayerAnimationController))]
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerController : StateMachine<State>
    {
        private PlayerAnimationController animationController;
        private PlayerMovementController movementController;
        private PlayerConfig playerConfig;
        private CameraConfig cameraConfig;
        private PlayerData data;

        public event Action OnPlayerDeath;
        public event Action OnPlayerFly;

        public override Dictionary<State, State<State>> Initalize()
        {
            return new Dictionary<State, State<State>>()
            {
                { State.Start, new State<State>(new Payload<State>(State.Start, Initialize)) },
                { State.Flying, new State<State>(new Payload<State>(State.Flying, Fly)) },
                { State.Dead, new State<State>(new Payload<State>(State.Dead, Die)) }
            };
        }

        protected override void Awake()
        {
            base.Awake();
            animationController = GetComponent<PlayerAnimationController>();
            movementController = GetComponent<PlayerMovementController>();
            playerConfig = Configuration.GetConfig<PlayerConfig>();
            cameraConfig = Configuration.GetConfig<CameraConfig>();
            data = Configuration.GetConfig<PlayerData>();
        }

        private void Start()
        {
            var camera = Camera.main;
            if(camera == null)
            {
                Debug.LogError("Camera is null!");
                return;
            }

            camera.transform.SetParent(transform);
            camera.transform.localPosition = cameraConfig.Position;
            camera.transform.localRotation = Quaternion.Euler(cameraConfig.Rotation);
        }

        public void Accelerate()
        {
            movementController.Accelerate();
        }

        public void Reset()
        {
            data.Reset();
            movementController.Reset();
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void Initialize()
        {
            Reset();
            transform.SetPositionAndRotation(playerConfig.BeginPosition, Quaternion.Euler(playerConfig.BeginEulerRotation));
            movementController.Disable();
            animationController.SetFly();
        }

        private void Fly()
        {
            OnPlayerFly?.Invoke();
            movementController.Enable();
            animationController.SetFly();
        }

        private void Die()
        {
            movementController.Disable();
            animationController.SetDie();
            OnPlayerDeath?.Invoke();
        }
    }
}

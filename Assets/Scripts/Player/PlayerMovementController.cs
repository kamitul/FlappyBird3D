using Config;
using Player.Movement.Commands;
using Player.Movement.Commands.Swipe;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour
    {
        private Rigidbody rgy;
        private InputConfig inputConfig;
        private Push push;
        private Jump jump;
        private ISwipe swipe;

        private bool isEnabled = false;

        private void Awake()
        {
            rgy = GetComponent<Rigidbody>();

            inputConfig = Configuration.GetConfig<InputConfig>();
            var playerConfig = Configuration.GetConfig<PlayerConfig>();
            var mapConfig = Configuration.GetConfig<MapConfig>();

            SetSwipeCommand(playerConfig, mapConfig);
            jump = new Jump(new Jump.Payload(rgy, playerConfig.VERTICAL_MOVEMENT_BOUNDS, playerConfig.JumpForce));
            push = new Push(new Push.Payload(rgy, playerConfig.ForwardSpeed, playerConfig.Accelerator));
        }

        private void SetSwipeCommand(PlayerConfig playerConfig, MapConfig mapConfig)
        {
            swipe = Application.platform == RuntimePlatform.Android ? new AndroidSwipe(new AndroidSwipe.Payload(rgy, inputConfig.Contact, inputConfig.Position,
                mapConfig.ZLines, playerConfig.StartLaneIndex)) : new KeyboardSwipe(new KeyboardSwipe.Payload(rgy, inputConfig.Direction, 
                playerConfig.HorizontalSpeed, playerConfig.HORIZONTAL_MOVEMENT_BOUNDS));
        }

        private void FixedUpdate()
        {
            if (isEnabled)
            {
                swipe.Execute();
                push.Execute();
            }
        }

        public void Enable()
        {
            inputConfig.Enable();
            rgy.useGravity = isEnabled = true;
        }

        public void Disable()
        {
            inputConfig.Disable();
            rgy.velocity = Vector3.zero;
            rgy.useGravity = isEnabled = false;
        }

        private void OnEnable()
        {
            inputConfig.Jump.action.performed += OnJumped;
        }

        private void OnDisable()
        {
            inputConfig.Jump.action.performed -= OnJumped;
        }

        private void OnJumped(InputAction.CallbackContext obj)
        {
            jump.Execute();
        }

        public void Accelerate()
        {
            push.Accelerate();
        }

        public void Reset()
        {
            push.Reset();
        }

        private void OnDestroy()
        {
            if (swipe is IDisposable disposable) disposable.Dispose();
        }
    }
}
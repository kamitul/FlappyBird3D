using UnityEngine;
using UnityEngine.InputSystem;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(InputConfig), menuName = "Input/Config")]
    public sealed class InputConfig : ScriptableObject
    {
        [field: SerializeField] public InputActionAsset Asset { get; private set; }
        [field: SerializeField] public InputActionReference Jump { get; private set; }
        [field: SerializeField] public InputActionReference Direction { get; private set; }
        [field: SerializeField] public InputActionReference Contact { get; private set; }
        [field: SerializeField] public InputActionReference Position { get; private set; }

        public void Enable()
        {
            Asset.Enable();
            Jump.action.Enable();
            Direction.action.Enable();
            Contact.action.Enable();
            Position.action.Enable();
        }

        public void Disable()
        {
            Asset.Disable();
            Jump.action.Disable();
            Direction.action.Disable();
            Contact.action.Disable();
            Position.action.Disable();
        }
    }
}

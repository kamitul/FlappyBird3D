using UnityEngine;
using UnityEngine.InputSystem;

namespace Config
{
    [CreateAssetMenu(fileName = nameof(InputConfig), menuName = "Input/Config")]
    public class InputConfig : ScriptableObject
    {
        [field: SerializeField] public InputActionAsset Asset { get; set; }
        [field: SerializeField] public InputActionReference Jump { get; set; }
        [field: SerializeField] public InputActionReference Direction { get; set; }
        [field: SerializeField] public InputActionReference Contact { get; set; }
        [field: SerializeField] public InputActionReference Position { get; set; }

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

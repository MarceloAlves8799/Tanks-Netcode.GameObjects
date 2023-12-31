using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace Tanks
{
    [CreateAssetMenu(fileName = "New Input Reader", menuName = "Inputs/Input Reader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        private Controls controls;

        public event Action<bool> PrimaryFireEvent;
        public event Action<Vector2> MoveEvent;

        public Vector2 AimPosition { get; private set; }


        void OnEnable()
        {
            if(controls == null)
            {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }

            controls.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PrimaryFireEvent?.Invoke(true);
            }

            else if (context.canceled)
            {
                PrimaryFireEvent?.Invoke(false);
            }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            AimPosition = context.ReadValue<Vector2>();
        }
    }
}

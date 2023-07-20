using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform tankBodyTransform;
        private Rigidbody playerRb;

        [Header("Movement")]
        [SerializeField] private float movementSpeed;
        private Vector2 previousMovementInput;

        [Header("Rotation")]
        [SerializeField] private float turningSpeed;


        private void Awake()
        {
            playerRb = GetComponent<Rigidbody>();
        }

        #region Netcode

        public override void OnNetworkSpawn()
        {
            if(!IsOwner) return;

            inputReader.MoveEvent += HandleMovement;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner) return;

            inputReader.MoveEvent -= HandleMovement;
        }

        #endregion

        private void Update()
        {
            if (!IsOwner) return;

            HandleRotation();
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;

            playerRb.velocity = tankBodyTransform.forward * previousMovementInput.y * movementSpeed;
        }

        private void HandleMovement(Vector2 movementInput)
        {
            previousMovementInput = movementInput;
        }

        private void HandleRotation()
        {
            float yRotation = previousMovementInput.x * turningSpeed * Time.deltaTime;
            tankBodyTransform.Rotate(0, yRotation, 0);
        }

    }
}

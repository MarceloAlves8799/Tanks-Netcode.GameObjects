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
        private Vector3 previousMovementInput;

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

            playerRb.velocity = previousMovementInput * movementSpeed;
        }

        private void HandleMovement(Vector2 movementInput)
        {
            previousMovementInput.Set(movementInput.x, 0, movementInput.y);
        }

        private void HandleRotation()
        {
            if(previousMovementInput == Vector3.zero) return;

            Quaternion yDirection = Quaternion.LookRotation(previousMovementInput);
            tankBodyTransform.rotation = Quaternion.Slerp(tankBodyTransform.rotation, yDirection, turningSpeed * Time.deltaTime);
        }


    }
}

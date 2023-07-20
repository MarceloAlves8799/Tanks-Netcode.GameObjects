using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class PlayerAiming : NetworkBehaviour
    {
        private Camera mainCamera;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform tankTurretTransform;

        [SerializeField] private float aimTurningSpeed;


        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if (!IsOwner) return;

            HandleAim();
        }

        private void HandleAim()
        {
            Ray aimRay = mainCamera.ScreenPointToRay(inputReader.AimPosition);
            Plane terrainPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (!terrainPlane.Raycast(aimRay, out rayDistance)) return;

            Vector3 aimWorldPosition = aimRay.GetPoint(rayDistance);
            Vector3 aimDirection = aimWorldPosition - tankTurretTransform.position;
            aimDirection.y = 0f; // Gets the direction XZ axis, ignore Y axis

            if (aimDirection == Vector3.zero) return;

            Quaternion directionToRotate = Quaternion.LookRotation(aimDirection);
            tankTurretTransform.rotation = Quaternion.Slerp(tankTurretTransform.rotation, directionToRotate, Time.deltaTime * aimTurningSpeed);


        }    
    }
}

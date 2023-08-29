using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

        private void OnLevelWasLoaded(int level)
        {
            if(level == 2)
            {
                mainCamera = Camera.main;
            }
        }

        private void LateUpdate()
        {
            if (!IsOwner) return;

            HandleAim();
        }

        private void HandleAim()
        {
            if(Gamepad.current == null)
            {
                HandleAimMouse();
            }

            else
            {
                Debug.Log("PS4 Connected");
                HandleAimGamepad();
            }
        }    

        private void HandleAimMouse()
        {

            if (mainCamera == null) return;

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

        private void HandleAimGamepad()
        {
            Vector3 rotationDirection = new Vector3(inputReader.AimPosition.x, 0f, inputReader.AimPosition.y);

            if(rotationDirection == Vector3.zero) return;

            float angle = Mathf.Atan2(rotationDirection.x, rotationDirection.z) * Mathf.Rad2Deg;
            tankTurretTransform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
    }
}

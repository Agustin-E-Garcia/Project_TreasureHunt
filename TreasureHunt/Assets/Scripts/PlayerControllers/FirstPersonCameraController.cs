using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureRun.PlayerControllers
{ 
    public class FirstPersonCameraController : MonoBehaviour
    {
        [Tooltip("Rotation speed of the character")]
        [SerializeField] private float rotationSpeed = 1.0f;
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        [SerializeField] private GameObject cinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        [SerializeField] private float topClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        [SerializeField] private float bottomClamp = -90.0f;

        private float _cinemachineTargetPitch;
        private float _currentRotationVelocity;
        private InputState _input;

        private const float _threshold = 0.01f;

        private void Start()
        {
            _input = InputState.instance;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void CameraRotation()
        {
            // if there is an input
            if (_input.look.sqrMagnitude >= _threshold)
            {
                
                _cinemachineTargetPitch += _input.look.y * rotationSpeed;
                _currentRotationVelocity = _input.look.x * rotationSpeed;

                // clamp our pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, bottomClamp, topClamp);

                // Update Cinemachine camera target pitch
                cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * _currentRotationVelocity);
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
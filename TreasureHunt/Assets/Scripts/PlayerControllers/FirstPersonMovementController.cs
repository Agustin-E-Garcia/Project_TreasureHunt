using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace TreasureRun.PlayerControllers
{
	[RequireComponent(typeof(CharacterController))]
	public class FirstPersonMovementController : MonoBehaviour
	{
		[Tooltip("Move speed of the character in m/s")]
		[SerializeField] private float moveSpeed = 4.0f;
		[Tooltip("Acceleration and deceleration")]
		[SerializeField] private float speedChangeRate = 10.0f;

		private float _speed;
		private CharacterController _controller;
		private InputState _input;

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = InputState.instance;
		}

		private void Update()
		{
			Move();
		}

		private void Move()
		{
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;

			if (currentHorizontalSpeed < moveSpeed - speedOffset || currentHorizontalSpeed > moveSpeed + speedOffset)
			{
				_speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed, Time.deltaTime * speedChangeRate);

				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = moveSpeed;
			}

			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			if (_input.move != Vector2.zero)
			{
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime));
		}
	}
}
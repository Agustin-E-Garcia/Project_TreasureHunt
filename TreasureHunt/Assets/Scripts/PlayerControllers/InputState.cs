using UnityEngine;
using System;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace TreasureRun.PlayerControllers
{
	public class InputState : MonoBehaviour
	{
		public static InputState instance = null;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool interact;
		public bool grab;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;

        private void Awake()
        {
			if (instance && instance != this)
				Destroy(this);
			else 
			{
				instance = this;
				// this should be handeled by a menu controller or something like that, for now I'll leave it here
				SetCursorState(CursorLockMode.Locked);
			}
        }

        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
				LookInput(value.Get<Vector2>());
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		private void SetCursorState(CursorLockMode newState)
		{
			Cursor.lockState = newState;
		}
	}	
}
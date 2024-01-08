using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool damage;
		public bool movement1;
		public bool movement2;
		public bool shoot;
		public bool aimDownSights;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

        public void OnDamage(InputValue value)
        {
            DamageInput(value.isPressed);
        }

        public void OnMovement1(InputValue value)
        {
            Movement1Input(value.isPressed);
        }

        public void OnMovement2(InputValue value)
        {
            Movement2Input(value.isPressed);
        }

        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        public void OnAimDownSights(InputValue value)
        {
            AimDownSightsInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        public void DamageInput(bool newDamageState)
        {
            damage = newDamageState;
        }

        public void Movement1Input(bool newMovement1State)
        {
            movement1 = newMovement1State;
        }

        public void Movement2Input(bool newMovement2State)
        {
            movement2 = newMovement2State;
        }

        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }

        public void AimDownSightsInput(bool newAimDownSightsState)
        {
            aimDownSights = newAimDownSightsState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}
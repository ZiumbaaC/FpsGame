using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualDamageInput(bool virtualDamageState)
        {
            starterAssetsInputs.DamageInput(virtualDamageState);
        }

        public void VirtualMovement1Input(bool virtualMovement1State)
        {
            starterAssetsInputs.Movement1Input(virtualMovement1State);
        }

        public void VirtualMovement2Input(bool virtualMovement2State)
        {
            starterAssetsInputs.Movement2Input(virtualMovement2State);
        }

        public void VirtualShootInput(bool virtualShootState)
        {
            starterAssetsInputs.ShootInput(virtualShootState);
        }

        public void VirtualAimDownSightsInput(bool virtualAimDownSightsState)
        {
            starterAssetsInputs.AimDownSightsInput(virtualAimDownSightsState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        
    }

}

using UnityEngine;

namespace PlayerSystem_Ver2
{
	internal class PlayerMoveState : PlayerStateBase
	{
		private readonly PlayerStateController _controller;
		private readonly Transform _playerCamera;

		private Vector2 _inputVector;
	
		internal PlayerMoveState(PlayerStateController controller, Vector2 inputVector)
		{
			_controller = controller;
			_playerCamera = controller.PlayerCamera;

			_inputVector = inputVector;
		}
	
		protected override void Enter()
		{
		
		}
	
		protected override void Execute()
		{
			DoMove();
		}
	
		protected override void Exit()
		{
		
		}
	
		protected override void Move(Vector2 inputVector)
		{
			_inputVector = inputVector;

			if (_inputVector.sqrMagnitude != 0)
			{
				// PlayAnimation();
			}
			else
			{
				// StopAnimation();
			}
		}
	
		protected override void Evade()
		{
			// Controller.ChangeState<PlayerEvadeState>();
		}
	
		protected override void Attack()
		{
			// Controller.ChangeState<PlayerAttackState>();
		}
	
		protected override void ActivateWeaponSkill()
		{
			// Controller.ChangeState<PlayerWeaponSkillState>();
		}
	
		protected override void ActivateUltimate()
		{
			// Controller.ChangeState<PlayerUltimateState>();
		}

		private void DoMove()
		{
			Vector3 moveVector = _inputVector.y * _playerCamera.forward + _inputVector.x * _playerCamera.right;
			moveVector.y = 0.0f;
			moveVector.Normalize();

			_controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, Quaternion.LookRotation(moveVector), 10.0f * Time.deltaTime * (1.0f / Time.timeScale));
		}
	}
}

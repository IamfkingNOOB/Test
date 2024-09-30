using UnityEngine;

namespace PlayerSystem_Ver2
{
	/// <summary>
	/// [클래스] 플레이어의 기본(Idle) 상태를 정의합니다.
	/// </summary>
	internal class PlayerIdleState : PlayerStateBase
	{
		#region 변수(필드)
		
		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [생성자] 변수를 초기화합니다.
		internal PlayerIdleState(PlayerStateController controller)
		{
			_controller = controller;
		}
		
		#region 재정의 함수 (IState)

		protected override void Enter()
		{
			Debug.Log("Idle 상태에 진입합니다!");
		}

		protected override void Execute()
		{
		
		}

		protected override void Exit()
		{
		
		}
		
		#endregion 재정의 함수 (IState)
		
		#region 재정의 함수 (IPlayerInput)

		protected override void Move(Vector2 inputVector)
		{
			if (inputVector.sqrMagnitude != 0.0f)
			{
				_controller.ChangeState(new PlayerMoveState(_controller, inputVector));
			}
		}

		protected override void Evade()
		{
			_controller.ChangeState(new PlayerEvadeState(_controller));
		}

		protected override void Attack()
		{
			// _controller.ChangeState(new PlayerAttackState(_controller));
		}

		protected override void ActivateWeaponSkill()
		{
			// _controller.ChangeState(new PlayerWeaponSkillState(_controller));
		}

		protected override void ActivateUltimate()
		{
			// _controller.ChangeState(new PlayerUltimateState(_controller));
		}
		
		#endregion 재정의 함수 (IPlayerInput)
		
		#endregion 함수(메서드)
	}
}

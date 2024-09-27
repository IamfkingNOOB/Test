using UnityEngine;

namespace PlayerSystem_Ver2
{
	internal class PlayerIdleState : PlayerStateBase
	{
		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;
		
		// [생성자] 변수를 초기화합니다.
		internal PlayerIdleState(PlayerStateController controller)
		{
			_controller = controller;
		}
	
		protected override void Enter()
		{
		
		}
	
		protected override void Execute()
		{
		
		}
	
		protected override void Exit()
		{
		
		}
	
		protected override void Move(Vector2 inputVector)
		{
			// _controller.ChangeState<PlayerMoveState>(inputVector);
			_controller.ChangeState(new PlayerMoveState(_controller, inputVector));
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
	}
}

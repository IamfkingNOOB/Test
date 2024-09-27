using UnityEngine;

namespace PlayerSystem_Ver2
{
	internal class PlayerHitState : PlayerStateBase
	{
		#region 변수(필드)
		
		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		// [변수] 피격한 몬스터
		private readonly MonsterAttacker _monster;

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [생성자] 변수를 초기화합니다.
		internal PlayerHitState(PlayerStateController controller, MonsterAttacker monster)
		{
			_controller = controller;
			_monster = monster;
		}


		protected override void Enter()
		{
			throw new System.NotImplementedException();
		}

		protected override void Execute()
		{
			throw new System.NotImplementedException();
		}

		protected override void Exit()
		{
			throw new System.NotImplementedException();
		}

		protected override void Move(Vector2 inputVector)
		{
			throw new System.NotImplementedException();
		}

		protected override void Evade()
		{
			throw new System.NotImplementedException();
		}

		protected override void Attack()
		{
			throw new System.NotImplementedException();
		}

		protected override void ActivateWeaponSkill()
		{
			throw new System.NotImplementedException();
		}

		protected override void ActivateUltimate()
		{
			throw new System.NotImplementedException();
		}
		
		#endregion 함수(메서드)
	}
}

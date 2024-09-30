using UnityEngine;

namespace PlayerSystem_Ver2
{
	/// <summary>
	/// [클래스] 플레이어의 무기 스킬(Weapon Skill) 상태를 정의합니다.
	/// </summary>
	internal class PlayerWeaponSkillState : PlayerStateBase
	{
		#region 변수(필드)

		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _weaponSkillAnimatorHash = Animator.StringToHash("WeaponSkill"); // 매개변수 해시 (Trigger)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerWeaponSkillState(PlayerStateController controller)
		{
			_controller = controller;
			
			if (!controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerIdleState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)
		
		protected override void Enter()
		{
			PlayAnimation(); // 애니메이션을 재생합니다.
		}

		protected override void Execute()
		{
			
		}

		protected override void Exit()
		{
			StopAnimation(); // 애니메이션을 정지합니다.
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
			_controller.ChangeState(new PlayerAttackState(_controller));
		}

		protected override void ActivateWeaponSkill()
		{
			// PlayAnimation();
		}

		protected override void ActivateUltimate()
		{
			_controller.ChangeState(new PlayerUltimateState(_controller));
		}

		#endregion 재정의 함수 (IPlayerInput)

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_weaponSkillAnimatorHash);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_weaponSkillAnimatorHash);
		}

		#endregion 함수(메서드)
	}
}

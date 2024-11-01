using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 필살기(Ultimate) 상태를 정의합니다.
	/// </summary>
	internal class PlayerUltimateState : PlayerStateBase
	{
		#region 변수(필드)

		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _weaponSkillAnimatorHash = Animator.StringToHash("Ultimate"); // 매개변수 해시 (Trigger)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerUltimateState(PlayerStateController controller)
		{
			_controller = controller;
			
			if (!controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerUltimateState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)
		
		protected override void Enter()
		{
			Debug.Log("[PlayerUltimateState] Enter() 함수가 호출되었습니다.");
			
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
			_controller.ChangeState(new PlayerWeaponSkillState(_controller));
		}

		protected override void ActivateUltimate()
		{
			// PlayAnimation();
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

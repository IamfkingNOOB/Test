using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 무기 스킬(Weapon Skill) 상태를 정의합니다.
	/// </summary>
	internal class PlayerDieState : PlayerStateBase
	{
		#region 변수(필드)
		
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _dieAnimatorHash = Animator.StringToHash("Die"); // 매개변수 해시 (Trigger)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerDieState(PlayerStateController controller)
		{
			if (!controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerDieState] 컴포넌트를 가져오는 데 실패했습니다!");
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

		// 쓰러진 후에는 아무것도 할 수 없습니다.
		protected override void Move(Vector2 inputVector) { }
		protected override void Evade() { }
		protected override void Attack() { }
		protected override void ActivateWeaponSkill() { }
		protected override void ActivateUltimate() { }

		#endregion 재정의 함수 (IPlayerInput)

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_dieAnimatorHash);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_dieAnimatorHash);
		}

		#endregion 함수(메서드)
	}
}

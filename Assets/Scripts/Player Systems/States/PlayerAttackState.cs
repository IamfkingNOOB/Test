using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 공격(Attack) 상태를 관리합니다.
	/// </summary>
	internal class PlayerAttackState : PlayerStateBase
	{
		#region 변수(필드)
		// [변수] 애니메이터(Animator)의 컴포넌트 및 매개변수
		private readonly Animator _animator; // 애니메이터의 컴포넌트
		private readonly int _attackAnimatorHash = Animator.StringToHash("Attack"); // 애니메이터의 매개변수 해시 (Trigger)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerAttackState(PlayerControllerBase controller) : base(controller)
		{
			bool isComponentFound = controller.TryGetComponent(out _animator);

			if (!isComponentFound)
				Debug.LogError("[PlayerIdleState] 컴포넌트를 가져오는 데 실패했습니다!");
		}

		#region 재정의 함수

		// [재정의 함수] IPlayerState 인터페이스 관련
		protected override void Enter()
		{
			PlayAnimation(); // 애니메이션을 재생합니다.
		}

		protected override void Exit()
		{
			StopAnimation(); // 애니메이션을 정지합니다.
		}
		
		// [재정의 함수] IPlayerInput 인터페이스 관련
		protected override void Attack()
		{
			InputBuffer = PlayAnimation; // 애니메이션을 재생합니다.
		}

		#endregion 재정의 함수

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_attackAnimatorHash);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_attackAnimatorHash);
		}

		#endregion 함수(메서드)
	}
}

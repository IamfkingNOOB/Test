using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 Idle 상태를 관리합니다.
	/// </summary>
	internal class PlayerMoveState : PlayerStateBase
	{
		#region 변수(필드)

		// [컴포넌트] 애니메이터(Animator)
		private readonly Animator _animator = default;

		// [변수] 애니메이터 매개변수
		private readonly int _isMoveAnimatorHash = Animator.StringToHash("isMove");

		#endregion 변수(필드)
	
		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		public PlayerMoveState(PlayerControllerBase controller) : base(controller)
		{
			bool isComponentFound = false;
			isComponentFound = controller.TryGetComponent(out _animator);
			
			if (!isComponentFound)
				Debug.LogError("[PlayerIdleState] 컴포넌트를 가져오는 데 실패했습니다!");
		}

		// [재정의 함수] IPlayerState 인터페이스 관련
		protected override void Enter()
		{
			// 애니메이션을 재생합니다.
			PlayAnimation();
		}

		protected override void Execute()
		{
		
		}

		protected override void Exit()
		{
			StopAnimation();
		}

		// [재정의 함수] IPlayerInput 인터페이스 관련
		protected override void Move()
		{
		
		}

		protected override void Evade()
		{
		
		}

		protected override void Attack()
		{
		
		}

		protected override void WeaponSkill()
		{
		
		}

		protected override void Ultimate()
		{
		
		}

		protected override void PetSkill()
		{
		
		}

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetBool(_isMoveAnimatorHash, true);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.SetBool(_isMoveAnimatorHash, false);
		}

		#endregion 함수(메서드)
	}
}

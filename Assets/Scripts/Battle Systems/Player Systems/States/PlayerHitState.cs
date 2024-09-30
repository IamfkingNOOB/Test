using MonsterSystem;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 피격(Hit) 상태를 정의합니다.
	/// </summary>
	internal class PlayerHitState : PlayerStateBase
	{
		#region 변수(필드)
		
		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;
		
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _hitAnimatorHash = Animator.StringToHash("Hit"); // 매개변수 해시 (Trigger)

		// [변수] 피격한 몬스터
		private readonly MonsterAttacker _monster;

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [생성자] 변수를 초기화합니다.
		internal PlayerHitState(PlayerStateController controller, MonsterAttacker monster)
		{
			_controller = controller;
			_monster = monster;

			if (!_controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerHitState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)

		protected override void Enter()
		{
			PlayAnimation();
			Debug.Log("Hit 상태에 진입합니다!");
		}

		protected override void Execute()
		{
			
		}

		protected override void Exit()
		{
			StopAnimation();
		}
		
		#endregion 재정의 함수 (IState)
		
		#region 재정의 함수 (IPlayerInput)

		protected override void Move(Vector2 inputVector)
		{
			
		}

		protected override void Evade()
		{
			
		}

		protected override void Attack()
		{
			
		}

		protected override void ActivateWeaponSkill()
		{
			
		}

		protected override void ActivateUltimate()
		{
			
		}
		
		#endregion 재정의 함수 (IPlayerInput)
		
		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_hitAnimatorHash);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_hitAnimatorHash);
		}
		
		#endregion 함수(메서드)
	}
}

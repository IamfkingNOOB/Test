using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PlayerSystem_Ver2
{
	/// <summary>
	/// [클래스] 플레이어의 회피(Evade) 상태를 관리합니다.
	/// </summary>
	internal class PlayerEvadeState : PlayerStateBase
	{
		#region 변수(필드)
		
		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;
		
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _evadeAnimatorHash = Animator.StringToHash("Evade"); // 매개변수 트리거(Trigger)

		// [변수] 회피 횟수
		private int _evadeCurrentCount = 0; // 현재 수행한 회피 횟수
		private const int EvadeMaxCount = 2; // 수행 가능한 연속 회피 횟수

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerEvadeState(PlayerStateController controller)
		{
			_controller = controller;

			if (!_controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerEvadeState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)

		protected override void Enter()
		{
			PlayAnimation(); // 애니메이션을 재생합니다.
			Debug.Log("Evade 상태에 진입합니다!");
		}

		protected override void Execute()
		{
			
		}

		protected override void Exit()
		{
			StopAnimation(); // 애니메이션을 정지합니다.
		}
		
		#endregion 재정의 함수 (IState)

		protected override void Move(Vector2 inputVector)
		{
			if (inputVector.sqrMagnitude != 0.0f)
			{
				_controller.ChangeState(new PlayerMoveState(_controller, inputVector));
			}
		}
		
		#region 재정의 함수 (IPlayerInput)

		// [재정의 함수] IPlayerInput 인터페이스 관련
		protected override void Evade()
		{
			if (_evadeCurrentCount < EvadeMaxCount) // 연속 회피가 가능할 때만,
			{
				PlayAnimation(); // 애니메이션을 재생합니다.
			}
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

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_evadeAnimatorHash);
			_evadeCurrentCount++;
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_evadeAnimatorHash);
			_evadeCurrentCount = 0;
		}

		#endregion 함수(메서드)
	}
}
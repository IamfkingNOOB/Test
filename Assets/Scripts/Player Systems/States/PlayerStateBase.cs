using System;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 상태를 관리하는 최상위 클래스입니다.
	/// </summary>
	internal abstract class PlayerStateBase : IPlayerState, IPlayerInputReceiver
	{
		#region 변수(필드)

		// [변수] 플레이어의 컨트롤러
		protected readonly PlayerControllerBase Controller;
		private readonly Animator _animator;
		
		// [변수] 선(先)입력 기능
		protected Action InputBuffer; // 입력 버퍼로, 모든 입력에 대한 행동은 이 버퍼를 통해 호출합니다.
		private bool _isPreInputTime; // 선입력 대기 시간으로, 그 시간이 지난 이후부터 버퍼의 값을 검사하여 호출합니다.

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		protected PlayerStateBase(PlayerControllerBase controller)
		{
			Controller = controller;

			bool isComponentFound = controller.TryGetComponent(out _animator);
			
			if (!isComponentFound)
				Debug.LogError("[PlayerStateBase] 컴포넌트를 가져오는 데 실패했습니다!");
		}
		
		#region 인터페이스 함수

		// [인터페이스 함수] IPlayerState 인터페이스
		void IPlayerState.Enter() => Enter();
		void IPlayerState.Execute() => Execute();
		void IPlayerState.Exit() => Exit();

		// [인터페이스 함수] IPlayerInput 인터페이스
		// void IPlayerInputReceiver.Move(Vector2 inputVector) => Move(inputVector);
		void IPlayerInputReceiver.Evade()
		{
			Evade();
		}

		void IPlayerInputReceiver.Attack() => Attack();
		void IPlayerInputReceiver.WeaponSkill() => WeaponSkill();
		void IPlayerInputReceiver.Ultimate() => Ultimate();
		void IPlayerInputReceiver.PetSkill() => PetSkill();

		// 추상화 함수로, 전반적인 기본 값을 할당합니다. (abstract → virtual)
		// [추상화 함수] IPlayerState 인터페이스 함수를 자식 클래스에서 재정의하기 위한 함수
		protected abstract void Enter();
		protected virtual void Execute() { CheckIdleAnimation(); InvokeInput(); }
		protected virtual void Exit() => ResetInput();

		// [추상화 함수] IPlayerInput 인터페이스 함수를 자식 클래스에서 재정의하기 위한 함수
		// protected abstract void Move(Vector2 inputVector);
		protected virtual void Evade() => InputBuffer = () => Controller.ChangeState<PlayerEvadeState>();
		protected virtual void Attack() => InputBuffer = () => Controller.ChangeState<PlayerAttackState>();
		protected virtual void WeaponSkill() => InputBuffer = () => Controller.ChangeState<PlayerWeaponSkillState>();
		protected virtual void Ultimate() => InputBuffer = () =>
		{
			if (Controller.BattleData.UseUltimate()) Controller.ChangeState<PlayerUltimateState>();
		};
		protected virtual void PetSkill() => InputBuffer = () => Controller.ChangeState<PlayerPetSkillState>();
		
		#endregion 인터페이스 함수
		
		#region 상태 클래스 공용 함수

		// [함수] 기본(Idle) 애니메이션에 진입했는지를 판별합니다.
		// 다른 상태에서, 오랫동안 입력이 없어 자동(Exit Time)으로 Idle 애니메이션으로 전환되었을 때, 상태도 전환해야 합니다.
		protected void CheckIdleAnimation()
		{
			if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
			{
				ResetInput(); // 입력 데이터를 초기화합니다.
				Controller.ChangeState<PlayerIdleState>(); // Idle 상태에 진입합니다.
			}
		}
		
		// [함수] 입력을 처리하는 함수로, 선입력 기능을 채용합니다.
		protected void InvokeInput()
		{
			if (!_isPreInputTime) // 선입력 시간이 지난 이후로부터,
			{
				if (InputBuffer != null) // 버퍼를 검사하여 입력된 값이 있다면,
				{
					InputBuffer?.Invoke(); // 입력을 처리합니다.
					ResetInput(); // 입력 데이터를 초기화합니다.
				}
				else if (IsMoving()) // 입력된 값이 없다면, 마지막으로 이동의 입력을 검사합니다.
				{
					Controller.ChangeState<PlayerMoveState>(); // 입력이 있을 경우, 이동 상태에 진입합니다.
				}
			}
		}

		// [함수] 입력 데이터를 초기화합니다.
		private void ResetInput()
		{
			InputBuffer = null; // 입력 버퍼를 지웁니다.
			// _isPreInputTime = true; // 선입력 시간에 진입합니다. [TODO: 지금은 애니메이션 클립에 이벤트가 없어 주석 처리함.]
		}
		
		// [함수] 지금 이동 입력이 있는지를 판별합니다.
		private bool IsMoving()
		{
			return Controller.InputVector.sqrMagnitude != 0.0f;
		}
		
		// [함수] 선입력 시간을 종료합니다. 애니메이션 클립에서 호출합니다.
		private void EndPreInputTime()
		{
			_isPreInputTime = false;
		}
		
		#endregion 상태 클래스 공용 함수

		#endregion 함수(메서드)
	}
}

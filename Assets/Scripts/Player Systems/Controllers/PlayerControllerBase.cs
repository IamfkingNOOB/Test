using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 조작을 관리합니다.
	/// </summary>
	internal class PlayerControllerBase : MonoBehaviour, IPlayerInput
	{
		#region 변수(필드)
		
		// [변수] 플레이어(발키리)의 데이터
		internal ValkyrieBattleData BattleData { get; private set; }

		// [변수] 상태 패턴에 사용하는 변수들
		private Dictionary<Type, PlayerStateBase> _stateCache; // 상태 클래스 목록을 캐시화합니다.
		private IPlayerState _currentState; // 플레이어의 상태를 지정 및 관리하기 위한 인터페이스
		private IPlayerInputReceiver _inputReceiver; // 상태 클래스의 입력 처리 함수에 접근하기 위한 인터페이스
		private IPlayerAnimationEvent _animationEvent; // 애니메이션 이벤트로 호출할 상태 클래스의 함수에 접근하기 위한 인터페이스

		// [변수] 이동과 관련한 입력의 값으로, 모든 상태가 일관된 값을 참조할 수 있도록 컨트롤러가 관리합니다.
		internal Vector2 InputVector { get; private set; }

		#endregion 변수(필드)

		#region 함수(메서드)

		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			InitializeCache(); // 캐시를 초기화합니다.
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			_currentState.Execute(); // 지금 상태의 실행 함수를 호출합니다.
		}

		// [유니티 충돌 이벤트 함수] OnTriggerEnter()
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Monster")) // 몬스터와 충돌했을 때,
			{
				if (_currentState is PlayerEvadeState evadeState) // 플레이어가 회피 중이라면,
				{
					evadeState.ActivateEvadeSkill(); // 회피 스킬을 발동합니다.
				}
				// else if (_currentState is PlayerDieState)
				// {
				// 	return;
				// }
				else // 그 외에는,
				{
					ChangeState<PlayerHitState>(); // 피격 상태가 됩니다.
				}
			}
		}

		// [함수] 상태 패턴에 사용할 상태 클래스들을 초기화하여, 캐시화합니다.
		private void InitializeCache()
		{
			_stateCache = new Dictionary<Type, PlayerStateBase>
			{
				{ typeof(PlayerIdleState), new PlayerIdleState(this) },
				{ typeof(PlayerMoveState), new PlayerMoveState(this) },
				{ typeof(PlayerEvadeState), new PlayerEvadeState(this) },
				{ typeof(PlayerAttackState), new PlayerAttackState(this) },
				{ typeof(PlayerWeaponSkillState), new PlayerWeaponSkillState(this) },
				{ typeof(PlayerUltimateState), new PlayerUltimateState(this) },
				{ typeof(PlayerPetSkillState), new PlayerPetSkillState(this) },
				// { typeof(PlayerHitState), new PlayerHitState(this) },
				// { typeof(PlayerDieState), new PlayerDieState(this) },
			};
			
			// 첫 상태는 Idle로 지정합니다.
			ChangeState<PlayerIdleState>();
		}
		
		// [함수] 플레이어의 데이터를 초기화합니다. (게임 매니저 등에서 호출합니다.)
		internal void InitializeData(ValkyrieBattleData data)
		{
			BattleData = data;
		}

		// [함수] 플레이어의 상태를 바꿉니다. 상태 클래스에서 호출합니다.
		internal void ChangeState<T>()
		{
			// 같은 상태로 바꾸고자 할 경우, 무시합니다.
			if (_currentState == _stateCache[typeof(T)]) return;
			
			_currentState?.Exit(); // 지금 상태의 종료 함수를 호출합니다.
		
			// 요청 받은 상태를 캐시에서 찾아서 적용합니다.
			_currentState = _stateCache[typeof(T)];
			_inputReceiver = _stateCache[typeof(T)];
			_animationEvent = _stateCache[typeof(T)];

			_currentState.Enter(); // 새로운 상태의 시작 함수를 호출합니다.
		}

		// [인터페이스 함수] 상태 클래스에 입력에 대한 값을 전달합니다.
		public void OnMove(InputAction.CallbackContext context) => InputVector = context.ReadValue<Vector2>();
		public void OnEvade(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Evade(); }
		public void OnAttack(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Attack(); }
		public void OnWeaponSkill(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.WeaponSkill(); }
		public void OnUltimate(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Ultimate(); }
		public void OnPetSkill(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.PetSkill(); }
		
		// [애니메이션 이벤트 함수] 키 입력에 대한 이벤트를 호출합니다. 애니메이션 이벤트는 private 함수에도 접근할 수 있습니다.
		private void OnStartIdleState() => _animationEvent.StartIdleState(); // Idle 애니메이션의 시작 지점에 호출합니다.
		private void OnStartInputStandby() => _animationEvent.StartInputStandby(); // 입력 지연이 필요한 애니메이션의 시작 지점에 호출합니다.
		private void OnStartPreInputTime() => _animationEvent.StartPreInputTime(); // 선입력을 시작하는 시점에 호출합니다.
		private void OnEndPreInputTime() => _animationEvent.EndPreInputTime(); // 선입력을 종료하는 시점에 호출합니다.
		
		#endregion 함수(메서드)
	}
}

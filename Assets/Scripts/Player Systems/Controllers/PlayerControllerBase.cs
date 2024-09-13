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
		public ValkyrieBattleData BattleData { get; private set; }

		// [변수] 상태 패턴에 사용하는 변수들
		private Dictionary<Type, PlayerStateBase> _stateCache; // 상태 클래스 목록을 캐시화합니다.
		private IPlayerState _currentState; // 플레이어의 상태를 지정 및 관리하기 위한 인터페이스
		private IPlayerInputReceiver _inputReceiver; // 상태 클래스의 입력 처리 함수에 접근하기 위한 인터페이스

		// [변수] 이동과 관련한 입력의 값으로, 모든 상태가 일관된 값을 참조할 수 있도록 컨트롤러가 관리합니다.
		public Vector2 InputVector { get; private set; }

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
			InputControl control;
			_currentState.Execute(); // 지금 상태의 실행 함수를 호출합니다.
		}

		// [유니티 충돌 이벤트 함수] OnTriggerEnter()
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Monster"))
			{
				// if (_currentState is PlayerEvadeState)
				// {
				// 	// 시공 단열을 연다.
				// 	(PlayerEvadeState)_currentState.OpenTimeMastery();
				// }
				// else if (_currentState is PlayerDieState)
				// {
				// 	return;
				// }
				// else
				// {
				// 	ChangeState<PlayerHitState>();
				// }
			}
		}

		// [함수] 상태 패턴에 사용할 상태 클래스들을 초기화하여, 캐시화합니다.
		private void InitializeCache()
		{
			_stateCache = new Dictionary<Type, PlayerStateBase>
			{
				{ typeof(PlayerIdleState), new PlayerIdleState(this) },
				{ typeof(PlayerMoveState), new PlayerMoveState(this) },
				// { typeof(PlayerEvadeState), new PlayerEvadeState(this) },
				// { typeof(PlayerAttackState), new PlayerAttackState(this) },
				// { typeof(PlayerWeaponSkillState), new PlayerWeaponSkillState(this) },
				// { typeof(PlayerUltimateState), new PlayerUltimateState(this) },
				// { typeof(PlayerPetSkillState), new PlayerPetSkillState(this) },
				// { typeof(PlayerHitState), new PlayerHitState(this) },
				// { typeof(PlayerDieState), new PlayerDieState(this) },
			};
			
			// 첫 상태는 Idle로 지정합니다.
			ChangeState<PlayerIdleState>();
		}
		
		// [함수] 플레이어의 데이터를 초기화합니다. (게임 매니저 등에서 호출합니다.)
		public void InitializeData(ValkyrieBattleData data)
		{
			BattleData = data;
		}

		// [함수] 플레이어의 상태를 바꿉니다. 상태 클래스에서 호출합니다.
		public void ChangeState<T>()
		{
			_currentState?.Exit(); // 지금 상태의 종료 함수를 호출합니다.
		
			// 요청 받은 상태를 캐시에서 찾아서 적용합니다.
			_currentState = _stateCache[typeof(T)];
			_inputReceiver = _stateCache[typeof(T)];

			_currentState.Enter(); // 새로운 상태의 시작 함수를 호출합니다.
		}

		// [인터페이스 함수] 상태 클래스에 입력에 대한 값을 전달합니다.
		public void OnMove(InputAction.CallbackContext context) => InputVector = context.ReadValue<Vector2>();
		public void OnEvade(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Evade(); }
		public void OnAttack(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Attack(); }
		public void OnWeaponSkill(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.WeaponSkill(); }
		public void OnUltimate(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.Ultimate(); }
		public void OnPetSkill(InputAction.CallbackContext context)  { if (context.performed) _inputReceiver.PetSkill(); }
		
		#endregion 함수(메서드)
	}
}

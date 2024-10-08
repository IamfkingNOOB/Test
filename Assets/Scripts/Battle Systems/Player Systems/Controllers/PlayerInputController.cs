using System;
using Frameworks.StatePattern;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 입력 시스템을 관리합니다.
	/// </summary>
	[RequireComponent(typeof(PlayerInput))]
	internal class PlayerInputController : MonoBehaviour
	{
		#region 변수(필드)

		// [변수] 상태 컨트롤러 및 현재 상태
		[SerializeField] private PlayerStateController stateController;
		private IPlayerInput _currentState;

		// [변수] 입력 처리를 위한 버퍼 및 대기 시간
		private Action _inputBuffer;
		private bool _isPreInputTime;

		// [변수] 이동 입력에 대한 값
		private Vector2 _inputVector;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			stateController.OnStateChanged += UpdateState; // 이벤트를 구독합니다.
		}

		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			stateController.OnStateChanged -= UpdateState; // 이벤트를 해제합니다.
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			InvokeInput(); // 입력을 처리합니다.
		}

		// [함수] 현재 상태를 갱신하는 이벤트를 정의합니다.
		private void UpdateState(IState changedState)
		{
			_currentState = changedState as IPlayerInput;
		}

		// [함수] 입력을 처리합니다.
		private void InvokeInput()
		{
			if (!_isPreInputTime) // 선입력 시간이 끝났을 때,
			{
				_inputBuffer?.Invoke(); // 입력 버퍼에 값이 들어 있다면 그것을 호출하고,
				_inputBuffer = null; // 버퍼를 비웁니다.
			}
		}

		#region 인풋 시스템(Input System)

		public void OnMove(InputAction.CallbackContext context)
		{
			Vector2 inputVector = context.ReadValue<Vector2>();
			_inputBuffer = () => _currentState?.Move(inputVector);
		}

		public void OnEvade(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				_inputBuffer = () => _currentState?.Evade();
			}
		}

		public void OnAttack(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				_inputBuffer = () => _currentState?.Attack();
			}
		}

		public void OnWeaponSkill(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				_inputBuffer = () => _currentState?.ActivateWeaponSkill();
			}
		}

		public void OnUltimate(InputAction.CallbackContext context)
		{
			/* 필살기 버튼이 누른 시간에 따라 다른 역할을 할 경우:
			if (context.started)
			{
				_inputBuffer = () => _currentState?.ActivateUltimate(false);
			}
			*/

			if (context.performed)
			{
				_inputBuffer = () => _currentState?.ActivateUltimate();
			}
		}

		#endregion 인풋 시스템(Input System)

		// [함수] 입력 대기 시간을 설정합니다. (애니메이션 이벤트에서 호출합니다.)
		internal void SetPreInputTime(bool value)
		{
			if (value)
			{
				_inputBuffer = null;
				_isPreInputTime = true;
			}
			else
			{
				_isPreInputTime = false;
			}
		}

		#endregion 함수(메서드)
	}
}

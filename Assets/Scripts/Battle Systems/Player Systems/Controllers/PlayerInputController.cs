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

		// [변수] 상태 컨트롤러 및 현재 상태(IPlayerInput 인터페이스)
		[SerializeField] private PlayerStateController stateController;
		private IPlayerInput _currentState; // 상태 컨트롤러의 IState 변수와 동기화

		// [변수] 입력을 처리하기 위한 버퍼 및 대기 시간
		private Action _inputBuffer;
		private bool _isPreInputTime;

		// [변수] 이동과 관련한 입력의 값
		private Vector2 _inputVector;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			stateController.StateChanged += OnUpdateState; // 이벤트를 등록합니다.
		}

		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			stateController.StateChanged -= OnUpdateState; // 이벤트를 해제합니다.
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			InvokeInput(); // 입력을 처리합니다.
		}

		// [콜백 함수] 상태 컨트롤러의 상태 변수의 값이 바뀔 때, 그 값을 동기화합니다.
		private void OnUpdateState(IState changedState)
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
			if (context.performed || context.canceled)
			{
				Vector2 inputVector = context.ReadValue<Vector2>();
				_inputBuffer = () => _currentState?.Move(inputVector);
			}
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

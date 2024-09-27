using System;
using Framework.StatePattern;
using UnityEngine;

namespace PlayerSystem_Ver2
{
	internal class PlayerStateController : MonoBehaviour
	{
		#region 변수(필드)

		private IState _currentState; // [변수] 플레이어의 현재 상태
		public event Action<IState> OnStateChanged; // [이벤트] 현재 상태가 바뀔 때 발생합니다.

		// [변수] 플레이어를 추적하는 카메라; Move 상태에서 필요합니다.
		[SerializeField] private Transform playerCamera;
		internal Transform PlayerCamera => playerCamera;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			_currentState.Execute(); // 현재 상태에 해당하는 행동을 취합니다.
		}

		// [함수] 플레이어의 상태를 바꿉니다.
		internal void ChangeState<T>(T nextState) where T : IState
		{
			_currentState?.Exit(); // 현재 상태를 끝내고,
			_currentState = nextState; // 상태를 바꾸고,
			_currentState.Enter(); // 바꾼 상태를 시작합니다.
				
			OnStateChanged?.Invoke(_currentState); // 상태가 변했다는 이벤트를 발생시킵니다.
		}

		#endregion 함수(메서드)
	}
}

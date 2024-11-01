using System;
using Frameworks.StatePattern;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 상태 시스템을 관리합니다.
	/// </summary>
	internal class PlayerStateController : MonoBehaviour
	{
		#region 변수(필드)

		// [변수] 플레이어를 추적하는 카메라; Move 상태에서 필요합니다.
		internal Transform PlayerCamera { get; private set; }
		
		// [변수] 플레이어의 현재 상태
		private IState _currentState;
		
		// [이벤트] 현재 상태가 바뀔 때 발생합니다.
		internal event Action<IState> StateChanged;
		
		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			PlayerCamera = Camera.main?.transform; // 플레이어의 카메라를 메인 카메라로 지정합니다.
		}
		
		// [유니티 생명 주기 함수] Start()
		private void Start()
		{
			ChangeState(new PlayerIdleState(this)); // 초기 상태를 설정합니다.
		}
		
		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			_currentState?.Execute(); // 현재 상태에 해당하는 행동을 취합니다.
		}
		
		// [함수] 플레이어의 상태를 바꿉니다.
		internal void ChangeState<T>(T nextState) where T : IState
		{
			// 현재 상태와 바꿀 상태가 같으면 즉시 종료합니다.
			if (_currentState?.GetType() == nextState.GetType()) return;
			
			_currentState?.Exit(); // 현재 상태를 끝내고,
			_currentState = nextState; // 상태를 바꾸고,
			_currentState.Enter(); // 바꾼 상태를 시작합니다.
			
			StateChanged?.Invoke(_currentState); // 상태가 바꼈다는 이벤트를 호출합니다.
		}
		
		#endregion 함수(메서드)
	}
}

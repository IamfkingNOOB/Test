using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 기본(Idle) 상태를 관리합니다.
	/// </summary>
	internal class PlayerIdleState : PlayerStateBase
	{
		// [생성자]
		internal PlayerIdleState(PlayerControllerBase controller) : base(controller) { }

		// ※ Idle 상태는 다른 상태에서 애니메이션의 Exit Time 등으로 스스로 전환되므로, 별도의 작업을 해주지 않습니다.
		protected override void Enter() { Debug.Log("Idle 상태에 진입합니다!"); }
		protected override void Exit() { }
	}
}

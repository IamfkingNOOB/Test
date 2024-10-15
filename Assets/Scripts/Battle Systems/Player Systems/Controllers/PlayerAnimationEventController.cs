using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 애니메이션 이벤트를 관리합니다.
	/// </summary>
	internal class PlayerAnimationEventController : MonoBehaviour
	{
		// [변수] 필요한 컨트롤러를 참조합니다.
		[SerializeField] private PlayerStateController stateController;
		[SerializeField] private PlayerInputController inputController;
		[SerializeField] private PlayerAttacker attacker;

		// [함수] Idle 애니메이션의 시작에 호출하여, Idle 상태에 진입합니다.
		private void OnStartIdleState()
		{
			stateController.ChangeState(new PlayerIdleState(stateController));
			inputController.SetPreInputTime(false);
		}

		// [함수] 입력 무시 시간에 진입합니다.
		private void OnStartInputIgnoreTime()
		{
			inputController.SetPreInputTime(true); // 애니메이션의 시작 즉시 호출하여, 일정 시간 동안 모든 입력을 무시합니다.
		}

		// [함수] 입력 대기 시간에 진입합니다.
		private void OnStartPreInputTime()
		{
			inputController.SetPreInputTime(true); // 애니메이션의 특정 시점에 호출하여, 일정 시간 동안 입력을 즉시 처리하지 않고, 대신 저장합니다.
		}

		// [함수] 입력 대기 시간을 종료합니다.
		private void OnEndPreInputTime()
		{
			inputController.SetPreInputTime(false); // 애니메이션의 특정 시점에 호출하여, 이후로 저장된 입력을 호출하거나, 입력을 즉시 처리합니다.
		}

		// [함수] 공격 애니메이션에서, 각 애니메이션에 대해 피해량의 배율을 설정합니다.
		private void OnSetDamageMagnification(int percentage)
		{
			attacker.DamageMagnification = percentage; // 공격 충돌체에 배율 값을 전달합니다.
		}
	}
}

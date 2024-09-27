using UnityEngine;

namespace PlayerSystem_Ver2
{
	public class PlayerAnimationEventController : MonoBehaviour
	{
		// [변수] 필요한 컨트롤러를 참조합니다.
		[SerializeField] private PlayerStateController stateController;
		[SerializeField] private PlayerInputController inputController;
	
		// [함수] Idle 애니메이션의 시작에 호출하여, Idle 상태에 진입합니다.
		private void StartIdleState()
		{
			stateController.ChangeState(new PlayerIdleState(stateController));
		}
	
		// [함수] 입력 대기 시간에 진입합니다.
		private void StartPreInputTime()
		{
			inputController.SetPreInputTime(true);
		}
	
		// [함수] 입력 대기 시간을 종료합니다.
		private void EndPreInputTime()
		{
			inputController.SetPreInputTime(false);
		}
	}
}

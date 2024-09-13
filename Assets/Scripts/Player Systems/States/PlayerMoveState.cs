using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 이동(Move) 상태를 관리합니다.
	/// </summary>
	internal class PlayerMoveState : PlayerStateBase
	{
		#region 변수(필드)

		// [변수] 캐릭터를 비추는 카메라 컴포넌트
		private readonly Transform _playerCamera;

		// [변수] 애니메이터(Animator)의 컴포넌트 및 매개변수
		private readonly Animator _animator; // 애니메이터의 컴포넌트
		private readonly int _isMoveAnimatorHash = Animator.StringToHash("isMove"); // 애니메이터의 매개변수 해시 (Bool)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		public PlayerMoveState(PlayerControllerBase controller) : base(controller)
		{
			_playerCamera = Camera.main?.transform; // 카메라는 메인 카메라(MainCamera 레이어)를 기준으로 합니다.

			bool isComponentFound = controller.TryGetComponent(out _animator);

			if (!isComponentFound)
				Debug.LogError("[PlayerIdleState] 컴포넌트를 가져오는 데 실패했습니다!");
		}

		#region 재정의 함수

		// [재정의 함수] IPlayerState 인터페이스 관련
		protected override void Enter()
		{
			PlayAnimation(); // 애니메이션을 재생합니다.
		}

		protected override void Execute()
		{
			base.Execute();

			if (IsMove())
				Move();
			else
				StopAnimation();
		}

		protected override void Exit()
		{
			base.Exit();
			StopAnimation(); // 애니메이션을 정지합니다.
		}

		#endregion 재정의 함수

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetBool(_isMoveAnimatorHash, true);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.SetBool(_isMoveAnimatorHash, false);
		}

		private bool IsMove()
		{
			Vector2 inputVector = Controller.InputVector; // 컨트롤러로부터 이동 벡터를 참조합니다.
			return inputVector.sqrMagnitude != 0.0f;
		}

		// [함수] 이동을 구현합니다.
		private void Move()
		{
			Vector2 inputVector = Controller.InputVector; // 컨트롤러로부터 이동 벡터를 참조합니다.
			
			// 카메라의 방향과 입력 값을 참조하여 이동 방향을 계산합니다.
			Vector3 moveVector = inputVector.y * _playerCamera.forward + inputVector.x * _playerCamera.right;
			moveVector.y = 0.0f; // Y축으로는 이동하지 않습니다.
			moveVector.Normalize(); // 값을 정규화합니다.

			// 플레이어를 해당 방향으로 자연스럽게 회전시킵니다.
			// 이동은 애니메이션의 루트 모션을 활용하여 구현하며, 입력이 없어도 후속 애니메이션이 있을 수 있음을 감안합니다.
			Controller.transform.rotation = Quaternion.Slerp(Controller.transform.rotation, Quaternion.LookRotation(moveVector), 10.0f * Time.deltaTime * (1.0f / Time.timeScale));
		}

		#endregion 함수(메서드)
	}
}

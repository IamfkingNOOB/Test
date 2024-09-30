using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 이동(Move) 상태를 정의합니다.
	/// </summary>
	internal class PlayerMoveState : PlayerStateBase
	{
		#region 변수(필드)

		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _moveAnimatorHash = Animator.StringToHash("IsMove"); // 매개변수 해시 (Bool)

		// [변수] 플레이어를 비추는 카메라
		private readonly Transform _playerCamera;

		// [변수] 이동 키의 입력 값
		private Vector2 _inputVector;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerMoveState(PlayerStateController controller, Vector2 inputVector)
		{
			_controller = controller;
			_playerCamera = controller.PlayerCamera;
			_inputVector = inputVector;

			if (!_controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerMoveState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)

		protected override void Enter()
		{
			Debug.Log("Move 상태에 진입합니다!");
		}

		protected override void Execute()
		{
			if (_inputVector.sqrMagnitude != 0) // 입력 값이 있으면,
			{
				DoMove(); // 이동을 처리합니다.
			}
			else // 없으면,
			{
				StopAnimation(); // 애니메이션을 정지합니다. (애니메이터에서 Idle로의 전이가 일어납니다.)
			}
		}

		protected override void Exit()
		{
			StopAnimation(); // 애니메이션을 정지합니다.
		}

		#endregion 재정의 함수 (IState)

		#region 재정의 함수 (IPlayerInput)

		protected override void Move(Vector2 inputVector)
		{
			_inputVector = inputVector;
		}

		protected override void Evade()
		{
			_controller.ChangeState(new PlayerEvadeState(_controller));
		}

		protected override void Attack()
		{
			_controller.ChangeState(new PlayerAttackState(_controller));
		}

		protected override void ActivateWeaponSkill()
		{
			_controller.ChangeState(new PlayerWeaponSkillState(_controller));
		}

		protected override void ActivateUltimate()
		{
			_controller.ChangeState(new PlayerUltimateState(_controller));
		}

		#endregion 재정의 함수 (IPlayerInput)

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetBool(_moveAnimatorHash, true);
		}

		// [함수] 애니메이션을 중지합니다.
		private void StopAnimation()
		{
			_animator.SetBool(_moveAnimatorHash, false);
		}

		// [함수] 이동을 처리합니다.
		private void DoMove()
		{
			// 애니메이션을 재생합니다.
			PlayAnimation();

			// 카메라의 시점을 기준으로 입력을 처리합니다.
			Vector3 moveVector = _inputVector.y * _playerCamera.forward + _inputVector.x * _playerCamera.right;
			moveVector.y = 0.0f;
			moveVector.Normalize();

			// 플레이어가 입력 방향으로 회전합니다.
			_controller.transform.rotation = Quaternion.Slerp(_controller.transform.rotation, Quaternion.LookRotation(moveVector), 10.0f * Time.deltaTime);
		}

		#endregion 함수(메서드)
	}
}

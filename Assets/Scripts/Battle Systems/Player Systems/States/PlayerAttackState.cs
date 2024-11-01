using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 공격(Attack) 상태를 정의합니다.
	/// </summary>
	internal class PlayerAttackState : PlayerStateBase
	{
		#region 변수(필드)

		// [변수] 상태 컨트롤러
		private readonly PlayerStateController _controller;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _weaponSkillAnimatorHash = Animator.StringToHash("Attack"); // 매개변수 해시 (Trigger)

		#endregion 변수(필드)

		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		internal PlayerAttackState(PlayerStateController controller)
		{
			_controller = controller;
			
			if (!controller.TryGetComponent(out _animator))
			{
				Debug.LogError("[PlayerAttackState] 컴포넌트를 가져오는 데 실패했습니다!");
			}
		}

		#region 재정의 함수 (IState)
		
		protected override void Enter()
		{
			Debug.Log("[PlayerAttackState] Enter() 함수가 호출되었습니다!");
			
			SetTarget(_controller); // 목표를 설정하고,
			PlayAnimation(); // 애니메이션을 재생합니다.
		}

		protected override void Execute()
		{
			
		}

		protected override void Exit()
		{
			StopAnimation(); // 애니메이션을 정지합니다.
		}
		
		#endregion 재정의 함수 (IState)
		
		#region 재정의 함수 (IPlayerInput)

		protected override void Move(Vector2 inputVector)
		{
			if (inputVector.sqrMagnitude != 0.0f)
			{
				_controller.ChangeState(new PlayerMoveState(_controller, inputVector));
			}
		}

		protected override void Evade()
		{
			_controller.ChangeState(new PlayerEvadeState(_controller));
		}

		protected override void Attack()
		{
			SetTarget(_controller);
			PlayAnimation();
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
			_animator.SetTrigger(_weaponSkillAnimatorHash);
		}

		// [함수] 애니메이션을 정지합니다.
		private void StopAnimation()
		{
			_animator.ResetTrigger(_weaponSkillAnimatorHash);
		}

		// [함수] 목표를 설정합니다.
		private void SetTarget(PlayerStateController controller)
		{
			Transform trueTarget = null;
			float detectDistance = 10.0f; // 목표를 설정할 수 있는 최대 거리
		
			// 일정 범위 안의 몬스터를 검출합니다.
			Collider[] targets = Physics.OverlapSphere(controller.transform.position, detectDistance, 1 << LayerMask.NameToLayer("Monster"));

			// 캐릭터의 시선에서 가장 가까운 몬스터를 고릅니다.
			foreach (Collider target in targets)
			{
				Vector3 from = controller.transform.position;
				Vector3 to = target.transform.position;

				float distance = Vector3.Distance(from, to);
			
				if (distance < detectDistance)
				{
					trueTarget = target.transform;
					detectDistance = distance;
				}
			}
		
			// 목표가 있을 경우,
			if (trueTarget)
			{
				//BattleManager.Instance.TargetMonster(trueTarget);
			}
		}

		#endregion 함수(메서드)
	}
}

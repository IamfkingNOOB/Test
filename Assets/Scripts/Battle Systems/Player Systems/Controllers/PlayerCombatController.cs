using Framework.StatePattern;
using MonsterSystem;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 전투 시스템을 관리합니다.
	/// </summary>
	internal class PlayerCombatController : MonoBehaviour
	{
		// [변수] 상태 컨트롤러 및 현재 상태
		[SerializeField] private PlayerStateController stateController;
		private IPlayerInput _currentState;

		// [변수] 플레이어의 스탯
		[SerializeField] private PlayerStatusController statusController;
		
		// [유니티 생명 주기 함수] OnDisable()
		private void OnEnable()
		{
			stateController.OnStateChanged += UpdateState; // 이벤트를 구독합니다.
		}

		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			stateController.OnStateChanged -= UpdateState; // 이벤트를 해제합니다.
		}
		
		// [함수] 현재 상태를 갱신하는 이벤트를 정의합니다.
		private void UpdateState(IState changedState)
		{
			_currentState = changedState as IPlayerInput;
		}
	
		// [유니티 이벤트 함수] OnTriggerEnter()
		private void OnTriggerEnter(Collider other)
		{
			// 몬스터의 공격에 피격했을 때,
			if (other.TryGetComponent(out MonsterAttacker monster))
			{
				if (_currentState is PlayerEvadeState evadeState) // 플레이어가 회피 상태라면,
				{
					evadeState.ActivateEvadeSkill(); // 회피 스킬을 발동합니다. TODO: 이것을 CombatController에서 정의하여 사용할까?
				}
				else // 그 외의 상태라면,
				{
					if (CalculateDamage(monster))
					{
						
					}
					
					// 피격 상태가 됩니다.
					stateController.ChangeState(new PlayerHitState(stateController, monster));
				}
			}
		}

		// [함수] 피해량을 계산합니다.
		private bool CalculateDamage(MonsterAttacker monster)
		{
			statusController.Status.DamageHealthPoint(10); // TODO: monster의 공격력으로 바꿀 것.
			return statusController.Status.CurrentHealthPoint > 0;
		}
	}
}

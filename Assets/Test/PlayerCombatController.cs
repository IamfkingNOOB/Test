using System;
using Framework.StatePattern;
using UnityEngine;

namespace PlayerSystem_Ver2
{
	/// <summary>
	/// [클래스] 플레이어의 전투 시스템을 관리합니다.
	/// </summary>
	internal class PlayerCombatController : MonoBehaviour
	{
		// [변수] 상태 컨트롤러 및 현재 상태
		[SerializeField] private PlayerStateController stateController;
		private IPlayerInput _currentState;
		
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
			if (other.TryGetComponent(out MonsterAttacker monsterAttacker))
			{
				if (_currentState is PlayerEvadeState) // 플레이어가 회피 상태라면,
				{
					// _currentState.ActivateEvadeSkill(); // 회피 스킬을 발동합니다.
				}
				else // 그 외의 상태라면,
				{
					// 피격 상태가 됩니다.
					stateController.ChangeState(new PlayerHitState(stateController, monsterAttacker));
				}
			}
		}
	}
}

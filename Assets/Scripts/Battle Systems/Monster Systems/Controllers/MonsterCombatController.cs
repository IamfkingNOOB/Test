using BattleSystem;
using PlayerSystem;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 전투 시스템을 관리합니다.
	/// </summary>
	internal class MonsterCombatController : MonoBehaviour
	{
		// [변수] 스탯 컨트롤러
		[SerializeField] private MonsterStatusController statusController;

		internal Transform Target { get; private set; } = BattleManager.Instance.CurrentPlayer; // [변수] 추적할 목표 대상
		internal bool IsHit { get; private set; } // [변수] 피격 여부 정보

		// [유니티 생명 주기 함수 함수] Awake()
		private void Awake()
		{
			ResetHitData(); // 피격 여부를 초기화합니다.
		}

		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			BattleManager.Instance.PlayerSwapped += OnTargetChanged; // 목표가 바뀔 때의 이벤트를 등록합니다.
		}

		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			BattleManager.Instance.PlayerSwapped -= OnTargetChanged; // 목표가 바뀔 때의 이벤트를 해제합니다.
		}

		// [유니티 이벤트 함수] OnTriggerEnter()
		private void OnTriggerEnter(Collider other)
		{
			// 플레이어에게 피격했을 경우,
			if (other.TryGetComponent(out PlayerAttacker player))
			{
				IsHit = true; // 피격 상태가 되고,

				int damage = CalculateDamage(statusController.Status, player.StatusController.Status); // 피해량을 계산하여,
				statusController.Status.DamageHP(damage); // 스탯에 적용합니다.
			}
		}

		// [함수] 피해량을 계산합니다.
		private int CalculateDamage(MonsterStatus monster, PlayerStatus player)
		{
			// 대충 계산식
			int damage = 0;
			return damage;
		}

		// [함수] 목표를 갱신합니다. 이벤트로 등록합니다.
		private void OnTargetChanged(Transform target)
		{
			Target = target;
		}

		// [함수] 피격 정보를 초기화합니다.
		internal void ResetHitData()
		{
			IsHit = false;
		}
	}
}

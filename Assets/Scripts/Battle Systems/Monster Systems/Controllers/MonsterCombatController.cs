using PlayerSystem;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 전투 시스템을 관리합니다.
	/// </summary>
	internal class MonsterCombatController : MonoBehaviour
	{
		// [변수] Status 컨트롤러
		[SerializeField] private MonsterStatusController statusController;
	
		// [변수] 필요 정보
		internal bool IsHit { get; private set; } // 피격 여부 정보
		internal Transform TargetTransform { get; private set; } // 목표 정보
	
		// [함수] Awake()
		private void Awake()
		{
			ResetHitData();
		}
	
		// [함수] OnTriggerEnter()
		private void OnTriggerEnter(Collider other)
		{
			// 플레이어에게 피격했을 경우,
			if (other.TryGetComponent(out PlayerStatusController player))
			{
				IsHit = true;
				TargetTransform = player.GetComponent<Transform>();
			
				int damage = CalculateDamage(player.Status);
				statusController.GetDamage(damage);
			}
		}
	
		// [함수] 피해량을 계산합니다.
		private int CalculateDamage(PlayerStatus status)
		{
			// 대충 계산식
			int damage = 0;
			return damage;
		}
	
		// [함수] 피격 정보를 초기화합니다.
		internal void ResetHitData()
		{
			IsHit = false;
		}
	}
}

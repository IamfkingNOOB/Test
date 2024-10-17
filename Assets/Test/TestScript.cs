using BattleSystem;
using PlayerSystem;
using UnityEngine;

public class TestScript : MonoBehaviour
{
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
			BattleManager.Instance.TargetMonster(trueTarget);
		}
	}
}
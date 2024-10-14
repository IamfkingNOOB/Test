using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	// [클래스] 몬스터가 플레이어를 공격할 수 있는 범위에 있는가를 판별하는 노드입니다.
	internal class MonsterIsNearToAttackNode : INode
	{
		// [변수] 몬스터의 컨트롤러
		private readonly MonsterCombatController _combat; // 전투
		private readonly MonsterStatusController _status; // 스탯
	
		// [변수] 몬스터의 위치(Transform)
		private readonly Transform _monsterTransform;
	
		// [생성자] 변수를 초기화합니다.
		internal MonsterIsNearToAttackNode(MonsterCombatController combat, MonsterStatusController status)
		{
			_combat = combat;
			_status = status;
			_monsterTransform = _combat.GetComponent<Transform>();
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			// 몬스터와 목표의 위치 값을 불러옵니다.
			Vector3 monsterPosition = _monsterTransform.position;
			Vector3 targetPosition = _combat.TargetTransform.position;
		
			// 불러온 값으로 거리를 계산하고, 그 값이 공격 범위 안에 있는가를 판단합니다.
			float distance = CalculateDistance(monsterPosition, targetPosition);
			NodeState nodeState = IsTargetInAttackRange(distance, _status.GetAttackRange());
		
			return nodeState;
		}
	
		// [함수] 몬스터와 목표 사이의 거리를 계산합니다.
		private float CalculateDistance(Vector3 monsterPosition, Vector3 targetPosition)
		{
			// 거리를 계산할 때, y축(상하)은 고려하지 않습니다.
			targetPosition = new Vector3(targetPosition.x, monsterPosition.y, targetPosition.z);
			// 몬스터와 목표 사이의 거리 값을 계산합니다.
			float calculatedDistance = Vector3.Distance(monsterPosition, targetPosition);
			
			return calculatedDistance;
		}
	
		// [함수] 목표가 몬스터의 공격 범위 안에 있는지를 확인합니다.
		private NodeState IsTargetInAttackRange(float distance, float attackRange)
		{
			// 공격 범위 안에 있다면 성공을, 없다면 실패를 반환합니다.
			NodeState nodeState = (distance <= attackRange) ? NodeState.Success : NodeState.Failure;
			
			return nodeState;
		}
	}
}

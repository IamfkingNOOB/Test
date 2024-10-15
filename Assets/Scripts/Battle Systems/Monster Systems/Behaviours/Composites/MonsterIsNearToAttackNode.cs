using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터가 플레이어를 공격할 수 있는 범위에 있는가를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsNearToAttackNode : INode
	{
		// [변수]
		private readonly Transform _monster; // 몬스터의 위치
		private readonly Transform _target; // 목표의 위치
		private readonly float _attackRange; // 공격 범위

		// [생성자] 변수를 초기화합니다.
		internal MonsterIsNearToAttackNode(Transform monster, Transform target, float attackRange)
		{
			_monster = monster;
			_target = target;
			_attackRange = attackRange;
		}

		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			float distance = CalculateDistance(_monster.position, _target.position); // 몬스터와 목표 사이의 거리를 계산하고,
			NodeState nodeState = IsTargetInAttackRange(distance, _attackRange); // 그 값이 공격 범위 안에 있는가를 판별합니다.

			return nodeState;
		}

		// [함수] 몬스터와 목표 사이의 거리를 계산합니다.
		private float CalculateDistance(Vector3 monster, Vector3 target)
		{
			target = new Vector3(target.x, monster.y, target.z); // y축(상하)은 고려하지 않습니다.
			float calculatedDistance = Vector3.Distance(monster, target); // 몬스터와 목표 사이의 거리를 계산합니다.

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
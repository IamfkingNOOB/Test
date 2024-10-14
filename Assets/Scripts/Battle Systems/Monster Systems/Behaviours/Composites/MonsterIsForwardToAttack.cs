using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	// [클래스] 몬스터가 플레이어를 공격할 수 있는 각도에 있는가를 판별하는 노드입니다.
	internal class MonsterIsForwardToAttackNode : INode
	{
		// [변수] 몬스터의 컨트롤러
		private readonly MonsterCombatController _combat;
		private readonly Transform _monsterTransform;
		
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _attackAnimatorHash = Animator.StringToHash("Attack");
		
		// [생성자] 변수를 초기화합니다.
		internal MonsterIsForwardToAttackNode(MonsterCombatController combat)
		{
			_combat = combat;
			_monsterTransform = _combat.GetComponent<Transform>();
			
			if (!_combat.TryGetComponent(out _animator))
			{
				Debug.LogError("[MonsterIsForwardToAttackNode] Animator 컴포넌트를 찾을 수 없습니다.");
			}
		}
		
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			NodeState nodeState;
			
			if (IsPlayingAttackAnimation(_animator)) // 지금 공격 애니메이션을 재생 중이라면,
			{
				nodeState = NodeState.Running; // 노드의 상태를 Running으로 반환합니다. (노드의 평가를 종료시킨다.)
			}
			else // 그 외에는,
			{
				Vector3 targetPosition = _combat.TargetTransform.position;
				nodeState = TurnToTarget(_monsterTransform, targetPosition); // 몬스터가 목표를 바라보고 있는지를 판별하여, 그에 맞는 노드의 상태를 반환합니다.
			}
			
			return nodeState;
		}
		
		// [함수] 지금 공격 애니메이션을 재생 중인지를 판별합니다.
		private bool IsPlayingAttackAnimation(Animator animator)
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
		}
		
		// [함수] 몬스터가 목표를 바라보도록 회전합니다. (공격을 수행하기 전, 몬스터가 플레이어를 바라보고 있어야 합니다.)
		private NodeState TurnToTarget(Transform monsterTransform, Vector3 target)
		{
			// 각도를 계산할 때, y축(상하)은 고려하지 않습니다.
			Vector3 monster = monsterTransform.position;
			monster = new Vector3(target.x, monster.y, target.z);

			// 몬스터와 플레이어 사이의 각도
			float viewAngle = Vector3.Angle(monsterTransform.forward, target - monster);

			NodeState nodeState;

			if (viewAngle > 10) // 그 각도가 약 좌우 각 10도 이상일 경우, (바라보고 있지 않다면)
			{
				// 목표를 바라보도록 회전합니다.
				Quaternion turnTo = Quaternion.LookRotation(target - monster);
				_monsterTransform.rotation = Quaternion.Slerp(_monsterTransform.rotation, turnTo, 2 * Time.deltaTime);
				
				nodeState = NodeState.Running; // 10도 미만일 때까지 계속합니다.
			}
			else // 아니라면,
			{
				nodeState = NodeState.Success; // 공격으로 넘어갑니다.
			}
			
			return nodeState;
		}
	}
}

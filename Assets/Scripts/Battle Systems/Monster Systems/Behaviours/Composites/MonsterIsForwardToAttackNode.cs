using Frameworks.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터가 플레이어를 공격할 수 있는 각도에 있는가를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsForwardToAttackNode : INode
	{
		// [변수]
		private readonly Transform _monster; // 몬스터의 위치
		private readonly Transform _target; // 목표의 위치

		// [변수] 애니메이터, NavMeshAgent
		private readonly Animator _animator;
		private readonly NavMeshAgent _navMeshAgent;

		// [생성자] 변수를 초기화합니다.
		internal MonsterIsForwardToAttackNode(Transform monster, Transform target, Animator animator, NavMeshAgent navMeshAgent)
		{
			_monster = monster;
			_target = target;
			_animator = animator;
			_navMeshAgent = navMeshAgent;
		}

		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			Debug.Log("MonsterIsForwardToAttackNode가 호출되었습니다.");
			
			NodeState nodeState = IsPlayingAttackAnimation(_animator) // 지금 공격 애니메이션을 재생 중이라면,
				? NodeState.Running // 노드의 상태를 Running으로 반환합니다. (노드의 평가를 종료시킨다.)
				: TurnToTarget(_monster, _target, _navMeshAgent); // 아니라면, 몬스터가 목표를 바라보고 있는지를 판별하여, 그에 맞는 노드의 상태를 반환합니다.

			return nodeState;
		}

		// [함수] 지금 공격 애니메이션을 재생 중인지를 판별합니다.
		private bool IsPlayingAttackAnimation(Animator animator)
		{
			return animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
		}

		// [함수] 몬스터가 목표를 바라보도록 회전합니다. (공격을 수행하기 전, 몬스터가 플레이어를 바라보고 있어야 합니다.)
		private NodeState TurnToTarget(Transform monster, Transform target, NavMeshAgent navMeshAgent)
		{
			Vector3 direction = target.position - monster.position; // 몬스터와 목표 사이의 방향을 계산합니다.
			direction.y = 0f; // 각도를 계산할 때, y축(상하)은 고려하지 않습니다.

			float viewAngle = Vector3.Angle(monster.forward, direction); // 몬스터와 플레이어 사이의 각도를 계산합니다.

			NodeState nodeState;

			if (viewAngle > 10.0f) // 그 각도가 약 좌우 각 10도 이상일 경우, (바라보고 있지 않다면)
			{
				// NavMesh로 제어합니다.
				navMeshAgent.updatePosition = false; // 이동은 제한하고, 회전은 허용하여 회전만 하도록 제어합니다. 
				navMeshAgent.SetDestination(target.position);

				nodeState = NodeState.Running; // 10도 미만일 때까지 계속합니다.
			}
			else // 아니라면,
			{
				navMeshAgent.updatePosition = true; // 이동의 제한을 해제합니다.
				navMeshAgent.isStopped = true; // NavMesh를 비활성화합니다.
				
				nodeState = NodeState.Success; // 공격으로 넘어갑니다.
			}

			return nodeState;
		}
	}
}

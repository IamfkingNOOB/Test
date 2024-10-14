using Frameworks.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 추적을 정의하는 노드입니다.
	/// </summary>
	internal class MonsterChaseNode : INode
	{
		// [변수] NavMeshAgent
		private readonly NavMeshAgent _navMeshAgent;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _chaseAnimatorHash = Animator.StringToHash("Chase");
	
		// [생성자] 변수를 초기화합니다.
		internal MonsterChaseNode(NavMeshAgent navMeshAgent, Animator animator)
		{
			_navMeshAgent = navMeshAgent;
			_animator = animator;
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			StartNavMesh(_navMeshAgent); // NavMesh를 시작하고,
			PlayAnimation(_animator); // 애니메이션을 재생합니다.
		
			return NodeState.Success; // 항상 성공 상태를 반환합니다.
		}
	
		// [함수] NavMesh를 시작합니다.
		private void StartNavMesh(NavMeshAgent navMeshAgent)
		{
			navMeshAgent.isStopped = false;
		}
	
		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation(Animator animator)
		{
			animator.SetBool(_chaseAnimatorHash, true);
		}
	}
}

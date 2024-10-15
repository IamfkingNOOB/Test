using System.Collections;
using Frameworks.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 사망을 정의하는 노드입니다.
	/// </summary>
	internal class MonsterDieNode : INode
	{
		// [변수] 몬스터의 컨트롤러 목록
		private readonly MonsterBehaviourTreeController _behaviourTree; // 행동 트리
		private readonly MonsterObjectPoolController _objectPool; // 오브젝트 풀링
	
		// [변수] NavMesh 컴포넌트
		private readonly NavMeshAgent _navMeshAgent;
	
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _dieAnimatorHash = Animator.StringToHash("Die"); // Bool

		// [생성자] 변수를 초기화합니다.
		internal MonsterDieNode(MonsterBehaviourTreeController behaviourTree, MonsterObjectPoolController objectPool, Animator animator, NavMeshAgent navMeshAgent)
		{
			_behaviourTree = behaviourTree;
			_objectPool = objectPool;
			_animator = animator;
			_navMeshAgent = navMeshAgent;
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			StopEvaluation(_behaviourTree); // 행동 트리의 평가를 중단하고,
			StopNavMesh(_navMeshAgent); // NavMesh를 비활성화하고,
			PlayAnimation(_animator, _dieAnimatorHash); // 애니메이션을 재생하고,
			_behaviourTree.StartCoroutine(ReleaseToPool(_objectPool)); // 오브젝트 풀에 돌려놓고,
		
			return NodeState.Success; // 항상 성공 상태를 반환합니다.
		}
	
		// [함수] 행동 트리의 평가를 중단합니다.
		private void StopEvaluation(MonsterBehaviourTreeController controller)
		{
			controller.IsStopped = true;
		}
	
		// [함수] NavMesh를 비활성화합니다.
		private void StopNavMesh(NavMeshAgent navMeshAgent)
		{
			navMeshAgent.isStopped = true;
		}
	
		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation(Animator animator, int animatorHash)
		{
			animator.SetBool(animatorHash, true);
		}
	
		// [코루틴] 애니메이션이 종료된 후, 오브젝트 풀에 돌려놓습니다.
		private IEnumerator ReleaseToPool(MonsterObjectPoolController controller)
		{
			yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsTag("Die")); // 사망 애니메이션이 재생 중인지를 확인합니다.
			yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f); // 애니메이션이 종료되었는지를 확인합니다.
		
			controller.Release(); // 오브젝트 풀에 돌려놓습니다.
		}
	}
}

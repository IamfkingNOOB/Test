using Frameworks.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 순찰을 정의하는 노드입니다.
	/// </summary>
	internal class MonsterWanderNode : INode
	{
		#region 변수(필드)

		// [변수] 몬스터의 위치 및 NavMeshAgent
		private readonly Transform _monster;
		private readonly NavMeshAgent _navMeshAgent;

		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _wanderAnimatorHash = Animator.StringToHash("Wander");	

		private const float WanderRadius = 5.0f; // [변수] 한 번에 돌아다니는 범위
		private const float WanderInterval = 5.0f; // [변수] 이동 목표를 바꾸는 주기(초)
		private float _elapsedTime; // [변수] 경과 시간

		#endregion 변수(필드)

		#region 함수(메서드)
		
		// [생성자] 변수를 초기화합니다.
		internal MonsterWanderNode(Transform monster, NavMeshAgent navMeshAgent, Animator animator)
		{
			_monster = monster;
			_navMeshAgent = navMeshAgent;
			_animator = animator;

			_elapsedTime = Time.time;
		}

		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			Debug.Log("MonsterWanderNode가 호출되었습니다.");
			
			if (Time.time - _elapsedTime > WanderInterval) // 일정 주기마다,
			{
				Wander(_navMeshAgent, _monster); // 주위를 순찰하고,
				_elapsedTime = Time.time; // 경과 시간을 갱신합니다.
			}

			PlayAnimation(_navMeshAgent, _animator); // 애니메이션을 재생합니다.

			return NodeState.Success; // 항상 성공 상태를 반환합니다.
		}

		// [함수] NavMesh로 움직일 수 있는, 근처의 무작위의 위치를 돌아다닙니다.
		private void Wander(NavMeshAgent navMeshAgent, Transform monster)
		{
			Vector3 nextPosition = monster.position; // 기본 위치는 자기 자신입니다.

			// 일정 횟수까지만 반복합니다. 반복 횟수 내에 위치를 정하지 못했을 경우, 제자리를 선택합니다.
			int tryCount = 30;
			for (int i = 0; i < tryCount; i++)
			{
				Vector3 randomPosition = nextPosition + Random.insideUnitSphere * WanderRadius; // 일정 범위 내의 위치를 무작위로 가져옵니다.

				if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, WanderRadius, NavMesh.AllAreas)) // 그 위치가 NavMesh 위에 있다면,
				{
					nextPosition = hit.position; // 그 값을 저장하고,
					i = tryCount; // 반복문을 종료합니다.
				}
			}

			// 찾은 다음 위치로 이동합니다.
			navMeshAgent.SetDestination(nextPosition);
		}

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation(NavMeshAgent navMeshAgent, Animator animator)
		{
			bool isMoving = navMeshAgent.remainingDistance > 0.1f; // 실제로 이동 중일 때만 애니메이션을 재생합니다.
			animator.SetBool(_wanderAnimatorHash, isMoving);
		}

		#endregion 함수(메서드)
	}
}

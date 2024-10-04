using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	internal class MonsterNavMeshController : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent navMeshAgent; // [변수] NavMeshAgent 컴포넌트
		[SerializeField] private float wanderRadius; // [변수] 한 번에 돌아다니는 범위
		
		private Transform _targetTransform; // [변수] 추적할 목표 위치

		private Coroutine _tempCoroutine;

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			if (_targetTransform)
			{
				StartChase();
			}
			else
			{
				StartWander();
			}
		}

		// [함수] 목표의 추적을 시작합니다.
		private void StartChase()
		{
			if (_targetTransform)
			{
				navMeshAgent.SetDestination(_targetTransform.position);
			}
		}
		
		// [함수] 주변의 순찰을 시작합니다.
		private void StartWander()
		{
			if (_tempCoroutine == null) _tempCoroutine = StartCoroutine(WanderOnNavMesh());
		}
		
		// [함수] 근처의 무작위의 위치로 이동합니다.
		private IEnumerator WanderOnNavMesh()
		{
			// 추적할 목표가 없으면 계속 반복합니다.
			while (!_targetTransform)
			{
				Vector3 nextPosition = transform.position;

				// 일정 횟수까지만 반복합니다. 반복 횟수 내에 위치를 정하지 못했을 경우, 제자리를 선택합니다.
				int tryCount = 30;
				for (int i = 0; i < tryCount; i++)
				{
					// 일정 범위 내의 위치를 무작위로 가져옵니다.
					Vector3 randomPosition = transform.position + Random.insideUnitSphere * wanderRadius;

					// 그 위치가 NavMesh 위일 경우, 그 값을 저장합니다.
					if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
					{
						nextPosition = hit.position;
						i = tryCount;
					}
				}

				// 찾은 다음 위치로 이동합니다.
				navMeshAgent.SetDestination(nextPosition);

				// 일정 시간 동안 기다립니다.
				yield return new WaitForSeconds(5.0f);
			}
		}
	}
}

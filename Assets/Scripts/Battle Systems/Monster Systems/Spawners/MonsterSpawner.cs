using Frameworks.ObjectPool;
using Frameworks.Singleton;
using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 오브젝트 풀링 시스템을 사용하여, 몬스터를 생성(Spawn)합니다.
	/// </summary>
	internal class MonsterSpawner : ObjectPoolManager<MonsterControllerBase>
	{
		// [변수] 몬스터를 생성할 위치의 범위 (이 컴포넌트가 부착된 오브젝트의 위치를 기준으로 합니다.)
		[SerializeField] private float spawnRadius;

		// Test
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Spawn();
			}
		}
		
		// [함수] 몬스터를 소환합니다.
		private void Spawn()
		{
			// 오브젝트 풀을 사용하여, 몬스터를 소환합니다.
			MonsterControllerBase spawned = Pool.Get();
			
			// 소환한 몬스터의 위치를 무작위로 지정합니다.
			spawned.transform.position = GetRandomPositionOnNavMesh();
		}

		// [함수] 몬스터를 소환할 수 있는 NavMesh 위의 위치 중에서, 무작위의 한 곳을 찾습니다.
		private Vector3 GetRandomPositionOnNavMesh()
		{
			Vector3 spawnPosition = transform.position;

			// 일정 횟수까지만 반복합니다.
			int tryCount = 30;
			for (int i = 0; i < tryCount; i++)
			{
				// 일정 범위 내의 위치를 무작위로 가져옵니다.
				Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;

				// 그 위치가 NavMesh 위일 경우, 그 값을 반환합니다.
				if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
				{
					spawnPosition = hit.position;
					i = tryCount;
				}
			}

			return spawnPosition;
		}
	}
}

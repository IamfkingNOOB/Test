using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터가 플레이어를 공격할 수 있는 범위 안에 있는지를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsNearToAttack : INode
	{
		private readonly MonsterControllerBase _controller;
		
		internal MonsterIsNearToAttack(MonsterControllerBase controller)
		{
			_controller = controller;
		}
		
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			return default;
		}

		// private bool IsAttackRange()
		// {
		// 	Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
		// }
		
		/// <summary>
		/// 몬스터와 플레이어 사이의 거리를 계산합니다.
		/// </summary>
		/// <param name="monster">몬스터의 위치 값</param>
		/// <param name="player">플레이어의 위치 값</param>
		/// <returns>몬스터와 플레이어 사이의 거리 값</returns>
		private float CalculateDistance(Vector3 monster, Vector3 player)
		{
			// 거리 계산 시 y축(상하)는 계산하지 않습니다.
			Vector3 monsterTransform = new(monster.x, 0, monster.z);
			Vector3 playerTransform = new(player.x, 0, player.z);

			// 적과 플레이어 사이의 거리를 계산하여, 반환합니다.
			float distance = Vector3.Distance(monsterTransform, playerTransform);
			return distance;
		}

		/// <summary>
		/// 플레이어가 몬스터의 공격 범위 안에 있는지를 확인합니다.
		/// </summary>
		/// <param name="distance">몬스터와 플레이어 사이의 거리</param>
		/// <param name="range">몬스터의 공격 범위</param>
		/// <returns>노드의 상태 값</returns>
		private NodeState CheckPlayerInAttackRange(float distance, float range)
		{
			// 거리가 공격 범위 안일 경우,
			if (distance <= range)
			{
				// 성공 상태를 반환합니다.
				return NodeState.Success;
			}
			// 아닐 경우,
			else
			{
				// 실패 상태를 반환합니다.
				return NodeState.Failure;
			}
		}
	}
}

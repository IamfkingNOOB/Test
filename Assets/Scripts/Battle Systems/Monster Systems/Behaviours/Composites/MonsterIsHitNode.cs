using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 피격 여부를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsHitNode : INode
	{
		// [변수] 전투 컨트롤러
		private readonly MonsterCombatController _combat;
	
		// [생성자] 변수를 초기화합니다.
		internal MonsterIsHitNode(MonsterCombatController combat)
		{
			_combat = combat;
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			Debug.Log("MonsterIsHitNode가 호출되었습니다.");
			
			NodeState nodeState = (_combat.IsHit) ? NodeState.Success : NodeState.Failure; // 피격 판별 변수를 참조하여 피격 여부를 판단합니다.
			_combat.ResetHitData(); // 판단 이후, 피격 판별 변수를 초기화합니다.
			
			return nodeState;
		}
	}
}

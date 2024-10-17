using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 사망 여부를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsDeadNode : INode
	{
		// [변수] 스탯 데이터
		private readonly MonsterStatus _status;
	
		// [생성자] 변수를 초기화합니다.
		internal MonsterIsDeadNode(MonsterStatus status)
		{
			_status = status;
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			Debug.Log("MonsterIsDeadNode가 호출되었습니다.");
			
			// 몬스터의 현재 체력이 0 이하일 경우, 사망한 것으로 판단합니다.
			NodeState nodeState = (_status.CurrentHP <= 0) ? NodeState.Success : NodeState.Failure;
			return nodeState;
		}
	}
}
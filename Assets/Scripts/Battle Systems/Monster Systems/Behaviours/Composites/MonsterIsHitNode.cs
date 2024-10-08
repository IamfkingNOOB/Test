using Frameworks.BehaviourTree;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터가 피격했는지를 판별하는 노드입니다.
	/// </summary>
	internal class MonsterIsHitNode : INode
	{
		// [변수] 몬스터의 컨트롤러
		private readonly MonsterControllerBase _controller;

		// [생성자] 필요한 변수를 초기화합니다.
		internal MonsterIsHitNode(MonsterControllerBase controller)
		{
			_controller = controller;
		}
		
		// [인터페이스 함수] 노드를 평가합니다. 
		NodeState INode.Evaluate() => IsHit() ? NodeState.Success : NodeState.Failure;

		// [함수] 몬스터의 피격 여부를 피격 변수로 판별합니다.
		private bool IsHit() => _controller.BattleData.IsHit;
	}
}

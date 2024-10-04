using Frameworks.BehaviourTree;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터가 쓰러졌는지를 판별하는 노드입니다.
	/// </summary>
	internal class IsDeadNode : INode
	{
		// [변수] 몬스터의 컨트롤러
		private readonly MonsterControllerBase _controller;

		// [생성자] 필요한 변수를 초기화합니다.
		internal IsDeadNode(MonsterControllerBase controller)
		{
			_controller = controller;
		}
		
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate() => CheckIsDead() ? NodeState.Success : NodeState.Failure;

		// [함수] 몬스터의 현재 체력이 0 이하일 경우, 쓰러진 것으로 간주합니다.
		private bool CheckIsDead() => _controller.BattleData.CurrentHealthPoint <= 0;
	}
}

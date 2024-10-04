using System.Collections.Generic;

namespace Frameworks.BehaviourTree
{
	/*
	 * Selector: 자식 노드를 순회하다가,
	 * Failure 상태인 노드를 만날 경우, 다음 노드를 평가한다.
	 * Success/Running 상태인 노드를 만날 경우, 그 노드까지만 실행하고 멈춘다.
	 */

	/// <summary>
	/// [클래스] 행동 트리(Behaviour Tree)의 셀렉터(Selector) 노드를 정의합니다.
	/// </summary>
	internal class SelectorNode : INode
	{
		// [변수] 자식 노드의 목록
		private readonly List<INode> _children;

		// [생성자] 자식 노드의 목록을 전달받습니다.
		internal SelectorNode(List<INode> children)
		{
			_children = children;
		}

		// [함수] 자식 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			// 만약 자식 노드가 없다면,
			if (_children == null || _children.Count == 0)
			{
				// Failure를 반환합니다.
				return NodeState.Failure;
			}

			// 자식 노드를 순회하면서,
			foreach (INode child in _children)
			{
				// 상태를 평가합니다.
				switch (child.Evaluate())
				{
					// Running일 경우, Running을 반환합니다.
					case NodeState.Running:
						return NodeState.Running;

					// Success일 경우, Success를 반환합니다.
					case NodeState.Success:
						return NodeState.Success;

					// Failure일 경우, 다음 노드를 평가합니다.
					default:
						continue;
				}
			}

			// 전부 순회했을 경우, Failure를 반환합니다. (아무것도 선택하여 실행하지 않았다는 뜻)
			return NodeState.Failure;
		}
	}
}

using System.Collections.Generic;

namespace Frameworks.BehaviourTree
{
	/*
	 * Sequence: 자식 노드를 순회하다가,
	 * Failure 상태인 노드를 만날 경우, 평가를 멈춥니다.
	 * Running 상태인 노드를 만날 경우, 다음 프레임에도 이 노드를 평가합니다.
	 * Success 상태인 노드를 만날 경우, 다음 프레임에는 다음 노드를 평가합니다.
	 */

	/// <summary>
	/// [클래스] 행동 트리(Behaviour Tree)의 시퀀스(Sequence) 노드를 정의합니다.
	/// </summary>
	internal class SequenceNode : INode
	{
		// [변수] 자식 노드의 목록
		private readonly List<INode> _children;

		// [생성자] 자식 노드의 목록을 전달받습니다.
		internal SequenceNode(List<INode> children)
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

					// Success일 경우, 다음 노드를 평가합니다.
					case NodeState.Success:
						continue;

					// Failure일 경우, Failure를 반환합니다.
					default:
						return NodeState.Failure;
				}
			}

			// 전부 순회했을 경우, Success를 반환합니다. (전부 실행했다는 뜻)
			return NodeState.Success;
		}
	}
}

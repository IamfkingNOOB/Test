namespace Framework.BehaviourTree
{
	// 노드의 상태를 나타내는 열거형
	internal enum NodeState
	{
		Success, Running, Failure
	}

	// 노드 클래스
	internal interface INode
	{
		// 노드의 상태를 평가하는 함수
		internal NodeState Evaluate();
	}
}

using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 사망을 처리하는 노드입니다.
	/// </summary>
	internal class MonsterDieNode : INode
	{
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _dieAnimatorHash = Animator.StringToHash("Die"); // 매개변수 해시 (Trigger)

		// [생성자] 필요한 변수를 초기화합니다.
		internal MonsterDieNode(MonsterControllerBase controller)
		{
			if (!controller.TryGetComponent(out _animator))
			{
				Debug.LogError("몬스터의 컨트롤러에서 애니메이터(Animator) 컴포넌트를 찾을 수 없습니다!");
			}
		}
		
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			PlayAnimation(); // 애니메이션을 재생합니다.
			
			return NodeState.Success; // 성공 상태를 반환합니다. (시퀀스의 마지막 노드)
		}

		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation()
		{
			_animator.SetTrigger(_dieAnimatorHash);
		}
	}
}

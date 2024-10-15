using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 피격을 정의하는 노드입니다.
	/// </summary>
	internal class MonsterHitNode : INode
	{
		// [변수] 애니메이터 및 매개변수
		private readonly Animator _animator;
		private readonly int _hitAnimatorHash = Animator.StringToHash("Hit");
	
		// [생성자] 변수를 초기화합니다.
		internal MonsterHitNode(Animator animator)
		{
			_animator = animator;
		}
	
		// [인터페이스 함수] 노드를 평가합니다.
		NodeState INode.Evaluate()
		{
			PlayAnimation(_animator, _hitAnimatorHash); // 애니메이션을 재생합니다.
			
			return NodeState.Success; // 항상 성공 상태를 반환합니다.
		}
	
		// [함수] 애니메이션을 재생합니다.
		private void PlayAnimation(Animator animator, int animatorHash)
		{
			animator.SetTrigger(animatorHash); // 애니메이션을 재생합니다.
		}
	}
}

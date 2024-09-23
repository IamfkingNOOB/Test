using Framework.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	internal class HitNode : INode
	{
		// [변수] 몬스터의 컨트롤러
		private MonsterControllerBase _controller;
		
		// [변수] 애니메이터(Animator)의 컴포넌트 및 매개변수
		private Animator _animator;
		private readonly int _hitAnimatorHash = Animator.StringToHash("Hit"); // 애니메이터의 매개변수 해시 (Trigger)

		// [생성자] 변수를 초기화합니다.
		internal HitNode(MonsterControllerBase controller)
		{
			_controller = controller;
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
			_animator.SetTrigger(_hitAnimatorHash); // 피격 애니메이션을 재생합니다.
			_controller.BattleData.IsHit = false; // 피격 여부를 초기화합니다.
		}
	}
}

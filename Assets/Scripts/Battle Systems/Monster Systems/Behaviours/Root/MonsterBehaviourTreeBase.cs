using System.Collections.Generic;
using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 행동 트리를 구성합니다.
	/// </summary>
	internal class MonsterBehaviourTreeBase : MonoBehaviour
	{
		#region 변수(필드)
		
		// [변수] 몬스터의 컨트롤러
		[SerializeField] private MonsterControllerBase controller;

		// [변수] 몬스터의 행동 트리에 사용할 최상위 노드
		private INode _rootNode;

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			if (controller)
				CreateTreeNode(); // 최상위 노드를 설정합니다.
			else
				Debug.LogError("[MonsterBehaviourTreeBase] MonsterControllerBase 컴포넌트를 찾을 수 없습니다!");
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			_rootNode?.Evaluate(); // 매 프레임마다 노드를 평가합니다.
		}

		// [함수] 노드를 구성합니다.
		private void CreateTreeNode()
		{
			/*
				                                              Root
				                                                ┃
				                                            Selector
				            ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
				        Selector                                                                Selector
				      ┏━━━━━┻━━━━━━┓                                ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━┓
				  Sequence     Sequence                         Sequence                        Sequence       Wander
				   ┏━━┻━━━┓     ┏━━┻━━━┓          ┏━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━┓          ┏━━━━━┻━━━━━┓
				IsDead → Die  IsHit → Hit  IsNearToAttack → IsForwardToAttack → Attack  IsNearToChase → Chase
            */
			
			_rootNode = new SelectorNode(new List<INode>
			{
				new SequenceNode(new List<INode>
				{
					new MonsterIsDeadNode(controller), new MonsterDieNode(controller)
				}),
				
				new SequenceNode(new List<INode>
				{
					new MonsterIsHitNode(controller), new MonsterHitNode(controller)
				})
			});
		}
		
		#endregion 함수(메서드)
	}
}

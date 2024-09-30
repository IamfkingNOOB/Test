using System.Collections.Generic;
using Framework.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 행동 트리를 구성합니다.
	/// </summary>
	internal class MonsterBehaviourTreeBase : MonoBehaviour
	{
		// [변수] 몬스터의 컨트롤러
		[SerializeField] private MonsterControllerBase controller;

		// [변수] 몬스터의 행동 트리에 사용할 최상위 노드
		private INode _rootNode;

		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			if (controller)
			{
				_rootNode = SetTreeNode(); // 최상위 노드를 설정합니다.
			}
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			_rootNode.Evaluate(); // 매 프레임마다 노드를 평가합니다.
		}

		// [함수] 노드를 구성합니다.
		private INode SetTreeNode()
		{
			/*
													                 Root
													                   ┃
												                   Selector
				             ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┻━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┓
			             Selector                                                                             Selector
		            ┏━━━━━━━━┻━━━━━━━━┓                                       ┏━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━┓
	            Sequence          Sequence                                Sequence                            Sequence       Patrol
	            ┏━━━┻━━━━┓       ┏━━━━┻━━━━┓            ┏━━━━━━━━━━━━━━━━━━━━━╋━━━━━━━━━━━━━━━┓            ┏━━━━━━┻━━━━━┓
            CheckDead → Die  CheckHit → GetHit  CheckNearToAttack → CheckForwardToAttack → Attack  CheckNearToChase → Chase
            */
			
			INode rootNode = new SelectorNode(new List<INode>
			{
				new SequenceNode(new List<INode>
				{
					new IsDeadNode(controller), new DieNode(controller)
				}),
				
				new SequenceNode(new List<INode>
				{
					
				})
			});

			return rootNode;
		}
	}
}

using System.Collections.Generic;
using Frameworks.BehaviourTree;
using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 행동 트리를 구성 및 평가합니다.
	/// </summary>
	internal class MonsterBehaviourTreeController : MonoBehaviour
	{
		#region 변수(필드)

		// [변수] 각 노드에 필요한 컨트롤러 데이터
		[SerializeField] private MonsterStatusController statusController;
		[SerializeField] private MonsterObjectPoolController objectPoolController;
		[SerializeField] private MonsterCombatController combatController;
		
		// [변수] 몬스터의 행동 트리에 사용할 최상위 노드
		private INode _rootNode;
		
        // [변수] 행동 트리의 평가 진행 여부
		public bool IsStopped { get; set; }

		#endregion 변수(필드)
		
		#region 함수(메서드)
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			_rootNode = CreateTree(); // 행동 트리를 생성합니다.
            IsStopped = false; // 평가를 시작합니다.
		}

		// [유니티 생명 주기 함수] Update()
		private void Update()
		{
			if (!IsStopped) // 평가가 중단되지 않았다면,
			{
				_rootNode.Evaluate(); // 매 프레임마다 행동 트리를 평가합니다.
			}
		}

		// [함수] 행동 트리를 구성 및 생성합니다.
		private INode CreateTree()
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
			
			INode newTree = new SelectorNode(new List<INode>
			{
				new SelectorNode(new List<INode>
				{
					new SequenceNode(new List<INode>
					{
						new MonsterIsDeadNode(statusController), new MonsterDieNode(this, objectPoolController)
					}),
					
					new SequenceNode(new List<INode>
					{
						new MonsterIsHitNode(combatController), new MonsterHitNode(combatController)
					})
				}),
				
				new SelectorNode(new List<INode>
				{
					new SequenceNode(new List<INode>
					{
						new MonsterIsNearToAttackNode(combatController, statusController), new MonsterIsForwardToAttackNode(combatController), //new MonsterAttackNode()
					}),
					
					new SequenceNode(new List<INode>
					{
						//new MonsterIsNearToChase(), new MonsterChaseNode()
					}),
					
					//new MonsterWanderNode()
				})
				
				
				
				
			});
			
			return newTree;
		}
		
		#endregion 함수(메서드)
	}
}

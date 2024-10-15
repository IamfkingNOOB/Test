using System.Collections.Generic;
using BattleSystem;
using Frameworks.BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 행동 트리를 구성 및 평가합니다.
	/// </summary>
	internal class MonsterBehaviourTreeController : MonoBehaviour
	{
		#region 변수(필드)

		#region 생성자 용도의 매개변수
		
		// [변수] 각 노드에 필요한 컨트롤러 및 컴포넌트
		[SerializeField] private MonsterStatusController statusController;
		[SerializeField] private MonsterObjectPoolController objectPoolController;
		[SerializeField] private MonsterCombatController combatController;

		[SerializeField] private Animator animator;
		[SerializeField] private NavMeshAgent navMeshAgent;
		
		#endregion 생성자 용도의 매개변수

		// [변수] 몬스터의 행동 트리에 사용할 최상위 노드
		private INode _rootNode;
		
        // [변수] 행동 트리의 평가 진행 여부
		internal bool IsStopped { get; set; }

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
						new MonsterIsDeadNode(statusController.Status),
						new MonsterDieNode(this, objectPoolController, animator, navMeshAgent)
					}),
					
					new SequenceNode(new List<INode>
					{
						new MonsterIsHitNode(combatController),
						new MonsterHitNode(animator)
					})
				}),
				
				new SelectorNode(new List<INode>
				{
					new SequenceNode(new List<INode>
					{
						new MonsterIsNearToAttackNode(transform, combatController.Target, statusController.Status.AttackRange),
						new MonsterIsForwardToAttackNode(transform, combatController.Target, animator, navMeshAgent),
						new MonsterAttackNode(animator)
					}),
					
					new SequenceNode(new List<INode>
					{
						new MonsterIsNearToChaseNode(transform, combatController.Target, statusController.Status.ChaseRange),
						new MonsterChaseNode(combatController.Target, animator, navMeshAgent)
					}),
					
					new MonsterWanderNode(transform, navMeshAgent, animator)
				})
			});
			
			return newTree;
		}

		#endregion 함수(메서드)
	}
}

using Cinemachine;
using UnityEngine;

namespace BattleSystem
{
	internal class CameraController : MonoBehaviour
	{
		// [변수] 시네머신의 타겟 그룹(Target Group) 카메라
		[SerializeField] private CinemachineTargetGroup targetGroup;

		// [변수] 플레이어의 카메라 설정 값
		[SerializeField] private float playerWeight;
		[SerializeField] private float playerRadius;

		// [변수] 목표가 된 몬스터의 카메라 설정 값
		[SerializeField] private float monsterWeight;
		[SerializeField] private float monsterRadius;

		// [유니티 생명 주기 함수] Start()
		private void Start()
		{
			AddPlayer(BattleManager.Instance.CurrentPlayer);
		}
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			// BattleManager.Instance.MonsterTargeted += AddTarget; // 이벤트를 등록합니다.
			// BattleManager.Instance.MonsterRemoved += RemoveTarget;
		}

		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			// BattleManager.Instance.MonsterTargeted -= AddTarget; // 이벤트를 해제합니다.
			// BattleManager.Instance.MonsterRemoved -= RemoveTarget;
		}

		// [함수] 플레이어를 멤버에 추가합니다.
		private void AddPlayer(Transform player)
		{
			targetGroup.AddMember(player, playerWeight, playerRadius);
		}

		// [함수] 플레이어를 멤버에서 제거합니다.
		private void RemovePlayer(Transform player)
		{
			targetGroup.RemoveMember(player);
		}

		// [함수] 몬스터를 멤버에 추가합니다.
		private void AddMonster(Transform monster)
		{
			targetGroup.AddMember(monster, monsterWeight, monsterRadius);
		}

		// [함수] 몬스터를 멤버에서 제거합니다.
		private void RemoveMonster(Transform monster)
		{
			targetGroup.RemoveMember(monster);
		}
	}
}

using System.Collections;
using Cinemachine;
using MonsterSystem;
using PlayerSystem;
using UnityEngine;

namespace BattleSystem
{
	/// <summary>
	/// [클래스] 출격 장면에서, 카메라를 조작합니다.
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		#region 변수(필드)
		
		// [변수] 시네머신(Cinemachine)
		[SerializeField] private CinemachineFreeLook freeLook; // FreeLook 카메라
		[SerializeField] private CinemachineTargetGroup targetGroup; // Target Group
		[SerializeField] private CinemachineVirtualCamera targetGroupCamera; // Target Group 카메라

		// [변수] 플레이어 및 몬스터의 관리자
		[SerializeField] private PlayerManager playerManager;
		[SerializeField] private MonsterManager monsterManager;
		
		// [변수] 플레이어의 카메라 관련 속성 값
		[SerializeField] private float playerWeight;
		[SerializeField] private float playerRadius;
		
		// [변수] 몬스터의 카메라 관련 속성 값
		[SerializeField] private float monsterWeight;
		[SerializeField] private float monsterRadius;
		
		// [변수] 현재 카메라가 목표(타겟)으로 삼고 있는 플레이어와 몬스터
		private Transform _currentPlayer;
		private Transform _currentMonster;
		
		#endregion 변수(필드)
		
		#region 유니티 생명 주기 함수
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			CheckFieldValidation(); // 변수의 유효성을 검사합니다.
		}
		
		// [유니티 생명 주기 함수] OnEnable()
		private void OnEnable()
		{
			Init(); // 변수와 카메라의 타겟 멤버를 초기화합니다.
			AddListeners(playerManager, monsterManager); // 필요한 이벤트를 등록합니다.
		}
		
		// [유니티 생명 주기 함수] OnDisable()
		private void OnDisable()
		{
			ClearTargetGroupMembers(); // 카메라에 저장한 모든 타겟 멤버를 제거합니다.
			RemoveListeners(playerManager, monsterManager); // 등록한 이벤트를 해제합니다.
		}
		
		#endregion 유니티 생명 주기 함수
		
		#region 이벤트
		
		// [함수] 필요한 이벤트를 등록합니다.
		private void AddListeners(PlayerManager player, MonsterManager monster)
		{
			player.PlayerSwapped += OnSwapPlayer;
			monster.MonsterTargeted += OnTargetMonster;
		}
		
		// [함수] 등록한 이벤트를 해제합니다.
		private void RemoveListeners(PlayerManager player, MonsterManager monster)
		{
			player.PlayerSwapped -= OnSwapPlayer;
			monster.MonsterTargeted -= OnTargetMonster;
		}
		
		// [콜백 함수] 플레이어가 교체될 때 호출됩니다.
		private void OnSwapPlayer(PlayerStatusController nextPlayer)
		{
			if (_currentPlayer)
			{
				// 교체하기 전의 플레이어를 Target Group 카메라의 멤버에서 제거합니다.
				targetGroup.RemoveMember(_currentPlayer);
			}
			
			_currentPlayer = nextPlayer.transform; // 교체할 플레이어를 저장합니다.
			
			if (nextPlayer)
			{
				UpdateFreeLookTarget(_currentPlayer); // 교체한 후의 플레이어를 FreeLook 카메라의 목표로 설정하고,
				targetGroup.AddMember(nextPlayer.transform, playerWeight, playerRadius); // Target Group 카메라의 멤버에 추가합니다.
			}
		}
		
		// [콜백 함수] 몬스터가 목표로 삼아질 때 호출됩니다.
		private void OnTargetMonster(MonsterStatusController nextMonster)
		{
			if (_currentMonster)
			{
				// 이전에 목표로 삼아졌던 몬스터를 Target Group 카메라의 멤버에서 제거합니다.
				targetGroup.RemoveMember(_currentMonster);
			}
			
			_currentMonster = nextMonster.transform; // 새롭게 목표가 된 몬스터를 저장합니다.

			if (nextMonster)
			{
				// 새로운 목표 몬스터를 Target Group 카메라의 멤버에 추가합니다.
				targetGroup.AddMember(nextMonster.transform, monsterWeight, monsterRadius);
			}

			// 메인 카메라를 일시적으로 Target Group 카메라로 전환합니다.
			StartCoroutine(FocusToTargetGroupCamera());
		}
		
		#endregion 이벤트
		
		#region 초기화
		
		// [함수] 변수의 유효성을 검사합니다.
		private void CheckFieldValidation()
		{
			bool isValid = !freeLook; // 모든 SerializeField 변수에 대해 Null 검사를 수행합니다.
			isValid = isValid && !targetGroup;
			isValid = isValid && !playerManager;
			isValid = isValid && !monsterManager;
			
			if (!isValid) // 어느 하나라도 변수가 유효하지 않을 경우,
			{
				Debug.LogError("[CameraController] SerializeField 변수가 할당되지 않았습니다."); // 오류 로그를 출력합니다.
			}
		}
		
		// [함수] 필요한 변수를 초기화합니다.
		private void Init()
		{
			// 현재 플레이어와 몬스터를 각 관리자의 것과 동기화합니다.
			_currentPlayer = playerManager.CurrentPlayer.transform;
			_currentMonster = monsterManager.CurrentMonster.transform;
			
			if (_currentPlayer) // 동기화한 값이 유효할 경우,
			{
				UpdateFreeLookTarget(_currentPlayer); // FreeLook 카메라의 목표로 설정합니다.
			}
			
			// 동기화한 값이 유효하고, Target Group 카메라의 멤버에 없을 경우,
			if (_currentPlayer && targetGroup.FindMember(_currentPlayer) == -1)
			{
				// Target Group 카메라의 멤버로 설정합니다.
				targetGroup.AddMember(_currentPlayer, playerWeight, playerRadius);
			}

			if (_currentMonster && targetGroup.FindMember(_currentMonster) == -1)
			{
				targetGroup.AddMember(_currentMonster, monsterWeight, monsterRadius);
			}
		}
		
		// [함수] Target Group 카메라에 저장한 모든 멤버를 제거합니다.
		private void ClearTargetGroupMembers()
		{
			foreach (CinemachineTargetGroup.Target member in targetGroup.m_Targets)
			{
				targetGroup.RemoveMember(member.target);
			}
		}
		
		#endregion 초기화
		
		// [함수] 카메라의 목표를 갱신합니다.
		private void UpdateFreeLookTarget(Transform target)
		{
			// FreeLook 카메라의 Follow와 LookAt 속성의 값으로 설정합니다.
			freeLook.Follow = target;
			freeLook.LookAt = target;
		}

		// [코루틴] Target Group 카메라로 전환합니다.
		private IEnumerator FocusToTargetGroupCamera()
		{
			// FreeLook 카메라보다 우선 순위를 높게 하여, 메인 카메라가 Target Group 카메라를 비추도록 합니다.
			targetGroupCamera.Priority = freeLook.Priority + 1; // 값이 클수록 우선 순위가 높습니다.
			
			yield return new WaitForSeconds(1.0f); // 일정 시간 동안 기다립니다.

			// 일정 시간 이후, 다시 메인 카메라가 FreeLook 카메라를 비추도록 합니다.
			targetGroupCamera.Priority = freeLook.Priority - 1;
		}
	}
}

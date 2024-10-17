using System;
using UnityEngine;

namespace PlayerSystem
{
	// [클래스] 플레이어의 전반적인 시스템(교체 등)을 관리합니다.
	internal class PlayerManager : MonoBehaviour
	{
		// [변수] 현재 플레이어 캐릭터
		internal Transform CurrentPlayer { get; private set; }
		
		// [이벤트] 플레이어 캐릭터가 교체될 때 호출됩니다.
		internal event Action<Transform, Transform> PlayerSwapped;

		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			GetLeader(); // 시작하기 전에, CurrentPlayer를 초기화합니다.
		}
		
		// [함수] 리더를 추가합니다.
		private void GetLeader()
		{
			CurrentPlayer = GetComponentInChildren<PlayerStatusController>(includeInactive: false).transform;
		}
		
		// [함수] 플레이어 캐릭터를 교체합니다.
		internal void SwapPlayer(Transform nextPlayer)
		{
			PlayerSwapped?.Invoke(CurrentPlayer, nextPlayer); // 관련 이벤트를 호출합니다.
			CurrentPlayer = nextPlayer; // 플레이어 캐릭터를 교체하고,
		}
	}
}

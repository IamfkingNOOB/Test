using System;
using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 전반적인 시스템(교체 등)을 관리합니다.
	/// </summary>
	internal class PlayerManager : MonoBehaviour
	{
		// [변수] 몬스터의 관리자
		[SerializeField] private MonsterManager monsterManager;
		
		
		
		// [변수 / 프로퍼티] 현재 조작 중인 플레이어
		private PlayerStatusController _currentPlayer;
		internal PlayerStatusController CurrentPlayer
		{
			get => _currentPlayer;
			private set
			{
				_currentPlayer = value;
				PlayerSwapped?.Invoke(_currentPlayer); // 플레이어를 교체할 때 이벤트를 호출합니다.
			}
		}

		// [이벤트] 플레이어를 교체할 때 호출합니다.
		internal event Action<PlayerStatusController> PlayerSwapped;

		// [함수] 플레이어를 교체합니다.
		internal void SwapPlayer(PlayerStatusController nextPlayer)
		{
			CurrentPlayer = nextPlayer;
		}
		
		// [함수] 플레이어들을 생성합니다.
		internal void InstantiatePlayers(PlayerStatusController[] players)
		{
			foreach (PlayerStatusController player in players)
			{
				PlayerStatusController newPlayer = Instantiate(player, transform);
				// newPlayer.InitStatus();
			}
		
			CurrentPlayer = players[0];
		}
	}
}

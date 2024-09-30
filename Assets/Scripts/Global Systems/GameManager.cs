using Frameworks.Singleton;
using PlayerSystem;
using UnityEngine;

internal class GameManager : SingletonMonoBehaviour<GameManager>
{
	[SerializeField] private PlayerUserInterface playerUI;
	
	internal void GetCurrentOnFieldPlayer(PlayerStateController controller)
	{
		// playerUI.SetPlayerData(controller.BattleData);
	}
}

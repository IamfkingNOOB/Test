using Framework.Singleton;
using PlayerSystem;
using UnityEngine;

internal class GameManager : SingletonMonoBehaviour<GameManager>
{
	[SerializeField] private PlayerUserInterface playerUI;
	
	internal void GetCurrentOnFieldPlayer(PlayerControllerBase controller)
	{
		playerUI.SetPlayerData(controller.BattleData);
	}
}

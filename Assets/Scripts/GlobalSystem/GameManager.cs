using System.Collections.Generic;
using DataBase;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void InitializePlayerParty(List<ValkyrieDataBase> playerParty)
	{
		foreach (var player in playerParty)
		{
			Instantiate(player.PrefabModel).SetActive(false);
		}
	}
}

using System;
using Frameworks.Singleton;
using UnityEngine;

namespace BattleSystem
{
	/// <summary>
	/// [클래스] 출전 중의 전체적인 시스템을 관리하는 매니저입니다.
	/// </summary>
	internal class BattleManager : Singleton<BattleManager>
	{
		internal Transform CurrentPlayer { get; private set; } // [변수] 현재 플레이어 캐릭터
		internal Transform TargetedMonster { get; private set; } // [변수] 목표로 하는 몬스터
	
		internal event Action<Transform> PlayerSwapped; // [이벤트] 플레이어 캐릭터가 교체될 때 호출됩니다.
		internal event Action<Transform> MonsterTargeted; // [이벤트] 목표로 하는 몬스터가 바뀔 때 호출됩니다.
		internal event Action<Transform> MonsterRemoved; // [이벤트] 목표로 하는 몬스터가 사라졌을 때 호출됩니다.
		
		// [함수] 플레이어 캐릭터를 교체합니다.
		internal void SwapPlayer(Transform nextPlayer)
		{
			CurrentPlayer = nextPlayer; // 플레이어 캐릭터를 교체하고,
			PlayerSwapped?.Invoke(CurrentPlayer); // 관련 이벤트를 호출합니다.
		}
	
		// [함수] 몬스터를 겨냥합니다.
		internal void TargetMonster(Transform monster)
		{
			TargetedMonster = monster; // 목표 몬스터를 바꾸고,
			MonsterTargeted?.Invoke(TargetedMonster); // 관련 이벤트를 호출합니다.
		}

		internal void RemoveTarget(Transform monster)
		{
			TargetedMonster = null;
			MonsterRemoved?.Invoke(monster);
		}

		
		// Test
		public void SetPlayer(Transform player)
		{
			CurrentPlayer = player;
		}
	}
}

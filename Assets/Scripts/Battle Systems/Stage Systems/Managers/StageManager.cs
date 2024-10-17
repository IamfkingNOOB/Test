using MonsterSystem;
using UnityEngine;

namespace StageSystem
{
	// [클래스] 무대의 정보를 관리합니다. (소환할 몬스터의 종류 및 수, 제한 시간 등)
	internal class StageManager : MonoBehaviour
	{
		// [변수] 몬스터의 종류
		[SerializeField, Tooltip("몬스터의 종류")] private MonsterStatus[] monsterTypes;

		// [변수] 몬스터의 소환 횟수
		[SerializeField, Tooltip("몬스터의 소환 횟수")] private float monsterSpawnCount;

		// [변수] 무대의 제한 시간 (초 단위; 100 = 1분 40초)
		[SerializeField, Tooltip("무대의 제한 시간 (초 단위)")] private int timeLimit;

		private void SetGameOver(bool isClear)
		{
			if (isClear)
			{
				Debug.Log("Stage Clear!!");
			}
			else
			{
				Debug.Log("Stage Fail!!");
			}
		}
	}
}

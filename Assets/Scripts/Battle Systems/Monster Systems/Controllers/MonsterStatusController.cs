using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 스탯(데이터)을 관리합니다.
	/// </summary>
	internal class MonsterStatusController : MonoBehaviour
	{
		// [변수] 몬스터의 스탯 데이터
		internal MonsterStatus Status { get; private set; } = new();

		// [함수] 스탯 데이터를 초기화합니다.
		internal void InitStatus(MonsterStatus status)
		{
			Status = status;
		}
	}
}

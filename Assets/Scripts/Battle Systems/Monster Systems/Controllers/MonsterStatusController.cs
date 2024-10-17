using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 스탯(데이터)을 관리합니다.
	/// </summary>
	internal class MonsterStatusController : MonoBehaviour
	{
		// [변수] 몬스터의 스탯 데이터
		[SerializeField] private MonsterStatus status;
		internal MonsterStatus Status => status;
	}
}

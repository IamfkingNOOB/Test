using UnityEngine;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 스탯을 관리합니다.
	/// </summary>
	internal class MonsterStatusController : MonoBehaviour
	{
		// [변수] 몬스터의 스탯 데이터
		private MonsterStatus _status;

		internal bool IsDead()
		{
			return _status.CurrentHealthPoint <= 0;
		}

		internal float GetAttackRange()
		{
			return _status.AttackRange;
		}

		internal void GetDamage(int damage)
		{
			_status.DamageHealthPoint(damage);
		}
	}
}

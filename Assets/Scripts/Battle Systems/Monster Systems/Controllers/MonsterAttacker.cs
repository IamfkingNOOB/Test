using UnityEngine;

namespace MonsterSystem
{
	internal class MonsterAttacker : MonoBehaviour
	{
		// [변수] 공격 피해량 (%)
		internal float Damage { get; private set; }

		internal void SetDamage(float percentage)
		{
			Damage = percentage;
		}
	}
}

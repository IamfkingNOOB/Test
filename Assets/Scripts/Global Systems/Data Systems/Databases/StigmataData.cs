namespace DataSystem
{
	internal class StigmataData
	{
		internal int ID { get; init; } // 식별자

		internal int HP { get; init; } // 체력
		internal int SP { get; init; } // 기력
		internal int ATK { get; init; } // 공격력
		internal int DEF { get; init; } // 방어력
		internal int CRT { get; init; } // 회심

		internal string ResourceName { get; init; } // 리소스 이름
	}
}

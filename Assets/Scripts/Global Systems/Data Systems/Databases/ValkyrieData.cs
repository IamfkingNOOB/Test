namespace DataSystem
{
	/// <summary>
	/// [클래스] 발키리(Valkyrie)의 기본 데이터를 관리합니다.
	/// </summary>
	internal class ValkyrieData
	{
		internal int ID { get; init; } // 식별자
		internal string CharacterName { get; init; } // 캐릭터 이름
		internal string SuitName { get; init; } // 슈트 이름
		internal string Type { get; init; } // 속성

		internal int HP { get; init; } // 체력
		internal int SP { get; init; } // 기력
		internal int ATK { get; init; } // 공격력
		internal int DEF { get; init; } // 방어력
		internal int CRT { get; init; } // 회심
		
		internal int WeaponID { get; init; } // 무기의 식별자
		internal int StigmataTopID { get; init; } // 성흔(상)의 식별자
		internal int StigmataMiddleID { get; init; } // 성흔(중)의 식별자
		internal int StigmataBottomID { get; init; } // 성흔(하)의 식별자
		
		internal string ResourceName { get; init; } // 리소스 이름
	}
}

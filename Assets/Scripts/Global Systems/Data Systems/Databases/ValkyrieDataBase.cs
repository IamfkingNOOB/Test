namespace EntityDatabase
{
	/// <summary>
	/// [클래스] 발키리(Valkyrie)의 기본 데이터를 관리합니다.
	/// </summary>
	internal class ValkyrieDataBase
	{
		internal int ID { get; init; } // 발키리의 식별자(ID)
		internal string Name { get; init; } // 발키리의 이름
		internal int HealthPoint { get; init; } // 발키리의 체력(HP)
		internal int SkillPoint { get; init; } // 발키리의 기력(SP)

		internal int UltimateCost { get; init; } // 발키리의 필살기 기력 소모량

		internal UnityEngine.GameObject PrefabModel{ get; init; } // 발키리의 모델 프리팹
	}
}

namespace EntityDatabase
{
	/// <summary>
	/// [클래스] 발키리(Valkyrie)의 기본 데이터를 관리합니다.
	/// </summary>
	internal class ValkyrieDataBase
	{
		public int ID { get; init; } // 발키리의 식별자(ID)
		public string Name { get; init; } // 발키리의 이름
		public int HealthPoint { get; init; } // 발키리의 체력(HP)
		public int SkillPoint { get; init; } // 발키리의 기력(SP)

		public int UltimateCost { get; init; } // 발키리의 필살기 기력 소모량

		public UnityEngine.GameObject PrefabModel{ get; init; } // 발키리의 모델 프리팹
	}
}

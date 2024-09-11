namespace DataBase
{
	/// <summary>
	/// [클래스] 발키리(Valkyrie)의 기본 데이터를 관리합니다.
	/// </summary>
	public class ValkyrieDataBase
	{
		public int ID { get; init; }
		public string Name { get; init; }
		public int HealthPoint { get; init; }
		public int SkillPoint { get; init; }

		public UnityEngine.GameObject PrefabModel{ get; init; }
	}
}

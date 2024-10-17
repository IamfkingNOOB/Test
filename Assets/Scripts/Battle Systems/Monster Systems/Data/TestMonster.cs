using UnityEngine;

namespace MonsterSystem
{
	[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/Monster")]
	internal class MonsterCreator : ScriptableObject
	{
		private string _privateName;
		[SerializeField] private string serializedName;
		public string publicName;
	}
}
using UnityEngine;

namespace MonsterSystem
{
	internal class MonsterControllerBase : MonoBehaviour
	{
		internal MonsterBattleData BattleData { get; private set; }

		private MonsterBehaviourTreeBase _behaviourTree;
	}
}

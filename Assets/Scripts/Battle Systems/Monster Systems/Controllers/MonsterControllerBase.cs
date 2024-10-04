using Frameworks.ObjectPool;
using PlayerSystem;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 MonoBehaviour 관련 이벤트를 관리합니다.
	/// </summary>
	internal class MonsterControllerBase : MonoBehaviour, IObjectPoolItem<MonsterControllerBase>
	{
		internal MonsterBattleData BattleData { get; private set; }
		
		#region 오브젝트 풀 (Object Pool)
		
		// [변수] 오브젝트 풀의 참조
		private IObjectPool<MonsterControllerBase> _pool;
		
		// [인터페이스 함수] 오브젝트 풀의 참조를 저장합니다. 오브젝트 풀링 매니저에서 호출합니다.
		void IObjectPoolItem<MonsterControllerBase>.GetPool(IObjectPool<MonsterControllerBase> pool)
		{
			_pool = pool;
		}
		
		#endregion 오브젝트 풀 (Object Pool)

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out PlayerCombatController player))
			{
				_pool.Release(this);
			}
		}
	}
}

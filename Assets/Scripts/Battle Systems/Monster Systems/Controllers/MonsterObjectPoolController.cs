using Frameworks.ObjectPool;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterSystem
{
	/// <summary>
	/// [클래스] 몬스터의 오브젝트 풀링을 관리합니다.
	/// </summary>
	internal class MonsterObjectPoolController : MonoBehaviour, IObjectPoolItem<MonsterObjectPoolController>
	{
		// [변수] 오브젝트 풀의 참조
		private IObjectPool<MonsterObjectPoolController> _pool;
		
		// [인터페이스 함수] 오브젝트 풀의 참조를 저장합니다. 오브젝트 풀링 매니저에서 호출합니다.
		void IObjectPoolItem<MonsterObjectPoolController>.GetPool(IObjectPool<MonsterObjectPoolController> pool)
		{
			_pool = pool;
		}

		// [함수] 이 게임 오브젝트를 오브젝트 풀에 돌려 놓습니다. (비활성화)
		internal void Release()
		{
			_pool.Release(this);
		}
	}
}

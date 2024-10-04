using UnityEngine;
using UnityEngine.Pool;

namespace Frameworks.ObjectPool
{
	/// <summary>
	/// [인터페이스] 오브젝트 풀링에 사용할 아이템이 특정 함수를 구현하도록 제약합니다.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal interface IObjectPoolItem<T> where T : MonoBehaviour
	{
		// [함수] 오브젝트 풀링 매니저의 참조를 저장합니다.
		internal void GetPool(IObjectPool<T> pool);
	}
}

using UnityEngine;
using UnityEngine.Pool;

namespace Frameworks.ObjectPool
{
	// [일반화 클래스] 오브젝트 풀링을 관리합니다.
	internal class ObjectPoolManager<T> : MonoBehaviour where T : MonoBehaviour, IObjectPoolItem<T>
	{
		[SerializeField] private T poolPrefab; // [변수] 오브젝트 풀링에 사용할 아이템의 프리팹
		private IObjectPool<T> _pool; // [변수] 오브젝트 풀링 인터페이스 (UnityEngine.Pool)

		// [변수] IObjectPool<T>의 매개변수들
		[SerializeField] private bool collectionCheck; 
		[SerializeField] private int defaultCapacity; // 기본 용량으로, 처음에 미리 생성해 둘 아이템의 개수를 설정합니다.
		[SerializeField] private int maxSize; // 최대 용량으로, 생성한 아이템의 개수가 최대 용량을 초과하면, 초과량은 반환(Release)할 때 대신 파괴(Destroy)됩니다.

		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			CheckPrefabValidation(); // 프리팹의 유효성(등록 여부)을 검사합니다.
			InitPool(); // 오브젝트 풀을 초기화합니다.
		}

		// [함수] 프리팹의 유효성(등록 여부)을 검사합니다.
		private void CheckPrefabValidation()
		{
			if (!poolPrefab) // 프리팹이 등록되어 있지 않으면,
			{
				Debug.LogError("프리팹을 등록하지 않았습니다!"); // 오류 로그를 출력합니다.
			}
		}

		// [함수] 오브젝트 풀을 초기화합니다.
		private void InitPool()
		{
			_pool = new ObjectPool<T>(
				createFunc: CreateItem, actionOnGet: OnGetItem,
				actionOnRelease: OnReleaseItem, actionOnDestroy: OnDestroyItem,
				collectionCheck: collectionCheck, defaultCapacity: defaultCapacity, maxSize: maxSize);
		}

		// [함수] 오브젝트 풀에 새 아이템을 생성합니다.
		private T CreateItem()
		{
			T newItem = Instantiate(poolPrefab); // 프리팹을 생성합니다.
			newItem.GetPool(_pool); // 아이템이 매니저가 가진 풀을 참조하도록 합니다.
			return newItem;
		}

		// [함수] 오브젝트 풀에서 아이템을 가져옵니다.
		private void OnGetItem(T item)
		{
			item.gameObject.SetActive(true); // 아이템을 활성화합니다.
		}

		// [함수] 오브젝트 풀에 아이템을 돌려놓습니다.
		private void OnReleaseItem(T item)
		{
			item.gameObject.SetActive(false); // 아이템을 비활성화합니다.
		}

		// [함수] 오브젝트 풀에 아이템을 돌려놓지 않고, 파괴합니다.
		private void OnDestroyItem(T item)
		{
			Destroy(item.gameObject); // 아이템을 파괴합니다.
		}
	}
}

using UnityEngine;

namespace Frameworks.Singleton
{
	/// <summary>
	/// [클래스] MonoBehaviour를 상속받는, 일반화 싱글톤(Singleton)
	/// </summary>
	/// <typeparam name="T">MonoBehaviour를 상속받는, 싱글톤을 상속받을 클래스</typeparam>
	internal class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		// [변수] 싱글톤을 상속받을 클래스의 객체(인스턴스)
		private static T _instance;
		
		// [속성] 싱글톤 객체의 접근을 제공합니다. 객체의 접근을 정적(static)으로 제한하여, 객체가 단 하나만 존재하도록 보장합니다.
		// 객체가 존재하지 않으면 지금 열려 있는 Scene에서 찾고, 찾지 못하면 해당 클래스를 컴포넌트로 부착한 게임 오브젝트를 새롭게 생성합니다.
		internal static T Instance => _instance ??= FindObjectOfType<T>() ?? new GameObject(typeof(T).Name).AddComponent<T>();

		// [유니티 생명 주기 함수] Awake()
		protected virtual void Awake()
		{
			// 이 클래스의 정적 변수가 비어 있다면, (처음 생성될 때)
			if (!_instance)
			{
				_instance = this as T; // 이 객체를 정적 변수가 가리키는 객체로 지정합니다.
				DontDestroyOnLoad(gameObject); // DontDestroyOnLoad()를 적용합니다. (Scene이 전환되어도 파괴되지 않습니다.)
			}
			// 정적 변수가 가리키는 객체가 이것이 아니라면, (이미 같은 클래스(컴포넌트)를 가진 게임 오브젝트가 Scene에 있을 때)
			else if (_instance != this)
			{
				Destroy(gameObject); // 중복된 이 게임 오브젝트를 파괴합니다.
			}
		}
	}
}

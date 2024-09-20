using System;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 싱글톤(Singleton) 일반화 클래스
    /// </summary>
    /// <typeparam name="T">상속받는 클래스</typeparam>
    internal class Singleton<T> where T : new()
    {
        // [변수/프로퍼티] Lazy<T>를 사용하여 스레드로부터 안전한, 게으른 생성을 사용합니다.
        private static readonly Lazy<T> _instance = new(() => new T());
        internal static T Instance => _instance.Value;

        // [함수] 생성자를 private/protected으로 제한하여 생성자를 통한 객체의 생성을 막습니다.
        protected Singleton() { }
    }
    
    /// <summary>
    /// MonoBehaviour를 상속받는 싱글톤 일반화 클래스
    /// </summary>
    /// <typeparam name="T">상속받는 클래스</typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        // [변수/프로퍼티] MonoBehaviour의 특징으로, 생성자를 사용하지 않는 등의 제약이 있습니다.
        private static readonly Lazy<T> _instance = new(CreateInstance);
        public static T Instance => _instance.Value;
        
        // [함수] Lazy<T>가 인스턴스를 생성할 때 호출하는 함수
        private static T CreateInstance()
        {
            T instance = FindAnyObjectByType<T>(); // 인스턴스가 존재하지 않을 경우, Find 함수로 씬 내에서 해당 컴포넌트가 부착된 게임 오브젝트를 찾습니다.
            instance ??= new GameObject(nameof(T)).AddComponent<T>(); // 여전히 존재하지 않을 경우, 새 게임 오브젝트를 생성하고, 해당 컴포넌트를 부착합니다.
            DontDestroyOnLoad(instance); // DontDestroyOnLoad()를 적용합니다.
            return instance;
        }

        // [유니티 생명 주기 함수] Awake()
        protected virtual void Awake()
        {
            // 인스턴스가 이미 생성되었으나, 현재 객체가 그 인스턴스가 아닐 경우, 이 게임 오브젝트를 파괴합니다.
            if (_instance.IsValueCreated && _instance.Value != this)
                Destroy(gameObject);
        }
    }
}

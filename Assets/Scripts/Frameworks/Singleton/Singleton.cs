using System;

namespace Frameworks.Singleton
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
}

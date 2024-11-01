using System.Collections;
using TMPro;
using UnityEngine;

namespace SettingSystem
{
	/// <summary>
	/// [클래스] 현재 FPS(Frame Per Second)를 표시합니다.
	/// </summary>
	internal class ScreenFrameRateIndicator : MonoBehaviour
	{
		#region 변수
		
		// [변수] 문자 UI
		[SerializeField] private TextMeshProUGUI text;
		
		// [변수] 표시기의 갱신 주기
		[SerializeField, Range(0.01f, 1.0f)] private float updateInterval;
		
		// [변수] 표시기의 활성화 여부
		private bool _isEnabled = default;
		
		#endregion 변수
		
		#region 함수
		
		// [유니티 생명 주기 함수] Start()
		private void Start()
		{
			_isEnabled = true; // 표시기를 활성화합니다.
			StartCoroutine(UpdateIndicator()); // 표시기를 갱신하는 코루틴을 시작합니다.
		}
		
		// [코루틴] 표시기를 갱신합니다.
		private IEnumerator UpdateIndicator()
		{
			while (_isEnabled) // 활성화되어 있는 동안,
			{
				text.SetText($"{(1.0f / Time.unscaledDeltaTime):F1} FPS"); // 표시기의 문자를 갱신합니다.
				yield return new WaitForSecondsRealtime(updateInterval); // 갱신 주기만큼 대기합니다.
			}
		}
		
		#endregion 함수
	}
}

using UnityEngine;
using UnityEngine.UI;

namespace SettingSystem
{
	/// <summary>
	/// [클래스] 창 모드 ↔ 전체 모드로 전환합니다.
	/// </summary>
	internal class FullScreenModeManager : MonoBehaviour
	{
		#region 변수
		
		// [변수] 토글 UI
		[SerializeField] private Toggle toggle;
		
		#endregion 변수
		
		#region 함수
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			InitToggle(); // 토글을 초기화합니다.
		}
		
		// [함수] 토글을 초기화합니다.
		private void InitToggle()
		{ 
			// 현재 화면의 모드 값으로 토글의 값을 설정합니다.
			toggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
		}
		
		// [토글 이벤트] OnValueChanged()
		internal void OnToggleValueChanged(bool value)
		{
			// 창 화면 ↔ 전체 화면으로 전환합니다.
			Screen.fullScreenMode = value ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
		}
		
		#endregion 함수
	}
}

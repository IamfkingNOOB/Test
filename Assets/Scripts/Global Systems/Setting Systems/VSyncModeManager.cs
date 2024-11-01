using UnityEngine;
using UnityEngine.UI;

namespace SettingSystem
{
	/// <summary>
	/// [클래스] 수직 동기화(VSync)를 설정합니다.
	/// </summary>
	internal class VSyncModeManager : MonoBehaviour
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
			// 수직 동기화의 활성화 여부로 토글의 값을 설정합니다.
			toggle.isOn = QualitySettings.vSyncCount > 0;
		}
	
		// [토글 이벤트] OnValueChanged()
		internal void OnToggleValueChanged(bool value)
		{
			// 수직 동기화를 활성화 ↔ 비활성화합니다.
			QualitySettings.vSyncCount = (value) ? 1 : 0;
		}
	
		#endregion 함수
	}
}

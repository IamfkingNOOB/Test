using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace SettingSystem
{
	/// <summary>
	/// [클래스] 목표 FPS를 설정합니다.
	/// </summary>
	internal class ScreenFrameRateManager : MonoBehaviour
	{
		#region 변수

		// [변수] 드롭다운 UI
		[SerializeField] private TMP_Dropdown dropdown;
		
		// [변수] 지원하는 주사율 목록
		private int[] _supportedRefreshRates = default;
		
		#endregion 변수
		
		#region 함수
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			GetSupportedRefreshRates(); // 지원하는 주사율들을 가져옵니다.
			AddOptionsToDropdown(); // 드롭다운에 목록을 추가합니다.
		}

		// [함수] 모니터가 지원하는 모든 주사율을 불러옵니다.
		private void GetSupportedRefreshRates()
		{
			// 모니터가 지원하는 모든 해상도를 불러옵니다.
			Resolution[] supported = Screen.resolutions;

			// 주사율만을 필요로 하므로, 공통된 해상도를 묶어서 걸러냅니다.
			_supportedRefreshRates = supported
				.Select(resolution => (int)Math.Round(resolution.refreshRateRatio.value))
				.Distinct()
				.OrderBy(refreshRate => refreshRate)
				.ToArray();
		}

		// [함수] 드롭다운에 주사율 목록을 추가합니다.
		private void AddOptionsToDropdown()
		{
			// 드롭다운의 모든 옵션을 지웁니다.
			dropdown.ClearOptions();

			// 주사율 목록을 원하는 포맷 형태로 드롭다운에 추가합니다.
			List<TMP_Dropdown.OptionData> options = _supportedRefreshRates
				.Select(refreshRate => new TMP_Dropdown.OptionData { text = $"{refreshRate}Hz" })
				.ToList();
			
			dropdown.AddOptions(options);
			
			// 드롭다운의 기본 값을 설정합니다.
			InitDropdown();
		}
		
		// [함수] 드롭다운의 기본 값을 설정합니다.
		private void InitDropdown()
		{
			// 현재 목표 FPS를 불러옵니다.
			int currentFrameRate = Application.targetFrameRate;

			// 드롭다운에서 그에 해당하는 값을 찾습니다.
			int currentValue = Array.FindIndex(_supportedRefreshRates, refreshRate => refreshRate == currentFrameRate);

			// 찾은 값을 드롭다운의 값으로 설정합니다.
			dropdown.value = currentValue;
		}

		// [드롭다운 이벤트] OnValueChanged()
		internal void OnDropdownValueChanged(int value)
		{
			// 선택한 옵션의 값을 주사율 목록에서 찾습니다.
			int refreshRate = _supportedRefreshRates[value];

			// 목표 FPS를 찾은 값으로 설정합니다.
			Application.targetFrameRate = refreshRate;
		}

		#endregion 함수
	}
}

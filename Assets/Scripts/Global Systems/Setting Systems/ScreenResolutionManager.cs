using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace SettingSystem
{
	/// <summary>
	/// [클래스] 해상도를 설정합니다.
	/// </summary>
	internal class ScreenResolutionManager : MonoBehaviour
	{
		#region 변수
		
		// [변수] 드롭다운 UI
		[SerializeField] private TMP_Dropdown dropdown;
		
		// [변수] 지원하는 해상도 목록
		private Vector2Int[] _supportedResolutions = default;
		
		#endregion 변수
		
		#region 함수
		
		// [유니티 생명 주기 함수] Awake()
		private void Awake()
		{
			GetSupportedResolutions(); // 지원하는 해상도들을 불러옵니다.
			AddOptionsToDropdown(); // 드롭다운에 목록을 추가합니다.
		}
		
		// [함수] 모니터가 지원하는 모든 해상도를 불러옵니다.
		private void GetSupportedResolutions()
		{
			// 모니터가 지원하는 모든 해상도를 불러옵니다.
			Resolution[] supported = Screen.resolutions;
			
			// 해상도만을 필요로 하므로, 공통된 주사율을 묶어서 걸러냅니다.
			_supportedResolutions = supported
				.Select(resolution => new Vector2Int(resolution.width, resolution.height))
				.Distinct()
				.OrderBy(size => size.x)
				.ToArray();
		}
		
		// [함수] 드롭다운에 해상도 목록을 추가합니다.
		private void AddOptionsToDropdown()
		{
			// 드롭다운의 모든 옵션을 지웁니다.
			dropdown.ClearOptions();
			
			// 해상도 목록을 원하는 포맷 형태로 드롭다운에 추가합니다.
			List<TMP_Dropdown.OptionData> options = _supportedResolutions
				.Select(resolution => new TMP_Dropdown.OptionData { text = $"{resolution.x} × {resolution.y}" })
				.ToList();
			
			dropdown.AddOptions(options);
			
			// 드롭다운의 기본 값을 설정합니다.
			InitializeDropdown();
		}
		
		// [함수] 드롭다운의 기본 값을 설정합니다.
		private void InitializeDropdown()
		{
			// 현재 해상도를 불러옵니다.
			var currentResolution = new Vector2Int(Screen.width, Screen.height);
			
			// 드롭다운에서 그에 해당하는 값을 찾습니다.
			int currentValue = Array.FindIndex(_supportedResolutions, resolution => resolution == currentResolution);
			
			// 찾은 값을 드롭다운의 값으로 설정합니다.
			dropdown.value = currentValue;
		}
		
		// [드롭다운 이벤트] OnValueChanged()
		internal void OnDropdownValueChanged(int value)
		{
			// 선택한 옵션의 값을 해상도 목록에서 찾습니다.
			int screenWidth = _supportedResolutions[value].x;
			int screenHeight = _supportedResolutions[value].y;
			
			// 해상도를 찾은 값으로 설정합니다.
			Screen.SetResolution(screenWidth, screenHeight, fullscreenMode: Screen.fullScreenMode);
		}
		
		#endregion 함수
	}
}

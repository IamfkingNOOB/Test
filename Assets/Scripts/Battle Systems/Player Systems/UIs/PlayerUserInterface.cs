using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 전투 장면에서, 플레이어의 정보(체력, 기력 등)를 표시합니다.
	/// </summary>
	internal class PlayerUserInterface : MonoBehaviour
	{
		#region 변수(필드)
	
		// [변수] 화면에 플레이어의 정보를 표시하는 UI(User Interface) 요소
		[SerializeField] private Slider healthPointBar;
		[SerializeField] private TextMeshProUGUI healthPointText;
		[SerializeField] private Slider skillPointBar;
		[SerializeField] private TextMeshProUGUI skillPointText;

		// [변수] UI가 사용할 플레이어의 정보
		private PlayerBattleData _playerData;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [함수] UI에 표시할 플레이어를 지정합니다. 다른 클래스에서 호출합니다.
		internal void SetPlayerData(PlayerBattleData playerData)
		{
			_playerData.PropertyChanged -= OnPropertyChanged; // 기존 플레이어의 이벤트에 등록되어 있던 함수를 해제합니다.
			_playerData = playerData; // 새로운 플레이어의 정보를 저장합니다.
			_playerData.PropertyChanged += OnPropertyChanged; // 새로운 플레이어의 이벤트에 함수를 등록합니다.
			
			UpdateUI(); // 한 번 강제로 UI를 갱신합니다.
		}

		// [함수] 플레이어 정보의 프로퍼티 변경 이벤트에 등록하는 콜백 함수로, 값이 바뀔 때 자동으로 호출됩니다.
		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			UpdateUI(e.PropertyName); // 값이 바뀌면, UI를 갱신합니다.
		}

		// [함수] 값이 바뀐 프로퍼티에 대응하는 UI를 갱신합니다.
		private void UpdateUI(string propertyName)
		{
			switch (propertyName)
			{
				case nameof(_playerData.CurrentHealthPoint): // 체력이 바뀌었을 경우,
					healthPointBar.value = _playerData.CurrentHealthPoint;
					healthPointText.SetText($"{_playerData.CurrentHealthPoint} / {_playerData.MaxHealthPoint}");
					break;
				case nameof(_playerData.CurrentSkillPoint): // 기력이 바뀌었을 경우,
					skillPointBar.value = _playerData.CurrentSkillPoint;
					skillPointText.SetText($"{_playerData.CurrentSkillPoint} / {_playerData.MaxSkillPoint}");
					break;
			}
		}

		// [함수] 프로퍼티가 변경되지 않았지만 UI를 갱신해야 할 필요가 있을 때, 강제로 모든 UI 요소를 갱신합니다.
		private void UpdateUI()
		{
			healthPointBar.value = _playerData.CurrentHealthPoint;
			healthPointText.SetText($"{_playerData.CurrentHealthPoint} / {_playerData.MaxHealthPoint}");
			skillPointBar.value = _playerData.CurrentSkillPoint;
			skillPointText.SetText($"{_playerData.CurrentSkillPoint} / {_playerData.MaxSkillPoint}");
		}

		#endregion 함수(메서드)
	}
}

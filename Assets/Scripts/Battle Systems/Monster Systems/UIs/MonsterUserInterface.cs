using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterSystem
{
	internal class MonsterUserInterface : MonoBehaviour
	{
		#region 변수(필드)
	
		// [변수] UI(User Interface) 요소
		[SerializeField] private CanvasGroup canvasGroup; // 아래의 UI를 포함하는 캔버스(Canvas)
		[SerializeField] private TextMeshProUGUI nameText; // 몬스터의 이름
		[SerializeField] private Slider healthPointBar; // 몬스터의 체력(HP)

		// [변수] UI가 사용할 몬스터의 스탯
		private MonsterStatus _status;

		#endregion 변수(필드)

		#region 함수(메서드)

		// [함수] UI가 사용할 몬스터를 설정합니다. 다른 클래스에서 호출합니다.
		internal void SetMonster(MonsterStatus status)
		{
			if (!ReferenceEquals(_status, status)) // 기존의 몬스터와 새로운 몬스터가 다를 때만,
			{
				_status.PropertyChanged -= OnPropertyChanged; // 기존 몬스터의 이벤트에 등록되어 있던 함수를 해제합니다.
				_status = status; // 새로운 몬스터를 저장합니다.
				_status.PropertyChanged += OnPropertyChanged; // 새로운 몬스터의 이벤트에 함수를 등록합니다.

				UpdateUI(); // 한 번은 UI를 직접 갱신합니다.
			}
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
				case nameof(_status.CurrentHP): // 체력이 바뀌었을 경우,
					healthPointBar.value = _status.CurrentHP;
					break;
			}
		}

		// [함수] 프로퍼티가 변경되지 않았지만 UI를 갱신해야 할 필요가 있을 때, 직접 모든 UI 요소를 갱신합니다.
		private void UpdateUI()
		{
			nameText.SetText($"{_status.Name}"); // 이름
			
			healthPointBar.maxValue = _status.MaxHP; // 최대 체력 값
			healthPointBar.value = _status.CurrentHP; // 현재 체력 값
		}

		private void HideUI()
		{
			
		}

		#endregion 함수(메서드)
	}
}

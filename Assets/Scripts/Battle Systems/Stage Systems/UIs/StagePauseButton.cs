using UnityEngine;
using UnityEngine.UI;

namespace StageSystem
{
	internal class StagePauseButton : MonoBehaviour
	{
		// [변수] 일시정지 버튼
		[SerializeField] private Button pauseButton;

		// [변수] 일시정지할 때 보여줄 UI 패널
		[SerializeField] private GameObject pausePanel;

		// [함수] 버튼의 클릭 이벤트
		public void OnClick()
		{
			Time.timeScale = 0f; // 게임을 일시정지하고,
			pausePanel.SetActive(true); // UI 패널을 활성화합니다.
		}
	}
}

using System;
using TMPro;
using UnityEngine;

namespace StageSystem
{
	// [클래스] 무대의 제한 시간을 관리합니다.
	internal class StageTimer : MonoBehaviour
	{
		// [변수] 무대 관리자
		[SerializeField] private StageManager stageManager;
		
		// [변수] 제한 시간을 보여주는 UI
		[SerializeField] private TextMeshProUGUI timerText;

		// [변수] 제한 시간
		[SerializeField] private float timeLimit;

		private void Update()
		{
			if (timeLimit > 0f) // 제한 시간이 남아 있을 경우,
			{
				timeLimit -= Time.deltaTime; // 게임 세계의 시간 흐름(Time Scale)에 영향을 받습니다.

				// 남은 제한 시간을 UI에 출력합니다.
				TimeSpan timeSpan = TimeSpan.FromSeconds(timeLimit);
				timerText.SetText($"{timeSpan.Minutes}:{timeSpan.Seconds:D2}"); // 출력 형태: M:SS
			}
			else // 제한 시간을 초과했을 경우,
			{
				timeLimit = 0f; // 남은 제한 시간을 '0초'로 고정하고,
				timerText.SetText($"0:00"); // UI의 표기 또한 '0:00'으로 고정합니다.
				enabled = false; // 이 컴포넌트를 비활성화합니다.
			}
		}
	}
}

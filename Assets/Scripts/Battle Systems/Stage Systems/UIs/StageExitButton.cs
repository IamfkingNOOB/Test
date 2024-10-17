using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StageSystem
{
	internal class StageExitButton : MonoBehaviour
	{
		// [변수] 종료 버튼
		[SerializeField] private Button exitButton;

		// [함수] 버튼의 이벤트 함수
		public void OnClick()
		{
			SceneManager.LoadScene("Main Scene"); // 메인 화면으로 돌아갑니다.
		}
	}
}

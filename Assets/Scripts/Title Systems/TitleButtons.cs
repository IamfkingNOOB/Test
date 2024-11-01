using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitleSystem
{
	internal class TitleButtons : MonoBehaviour
	{
		// [버튼 이벤트] 출격 화면으로 이동합니다.
		public void OnClickToBattle()
		{
			SceneManager.LoadSceneAsync("Battle Scene");
		}
	}
}

using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 스탯(데이터)를 관리합니다.
	/// </summary>
	internal class PlayerStatusController : MonoBehaviour
	{
		// [변수] 플레이어의 스탯 데이터
		public PlayerStatus Status { get; private set; }

		// [함수] 스탯 데이터를 초기화합니다.
		internal void InitStatus(PlayerStatus status)
		{
			Status = status;
		}
	}
}

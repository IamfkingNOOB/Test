using UnityEngine;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 스탯(데이터)를 관리합니다.
	/// </summary>
	internal class PlayerStatusController : MonoBehaviour
	{
		public PlayerStatus Status { get; private set; }

		internal void InitStatus(PlayerStatus status)
		{
			Status = status;
		}
	}
}

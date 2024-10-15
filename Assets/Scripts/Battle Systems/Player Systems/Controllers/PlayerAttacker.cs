using UnityEngine;

namespace PlayerSystem
{
	internal class PlayerAttacker : MonoBehaviour
	{
		// [변수 / 프로퍼티] 플레이어의 스탯 데이터
		[SerializeField] private PlayerStatusController statusController; 
		internal PlayerStatusController StatusController => statusController; 
		
		// [변수] 충돌 시 공격이 명중한 것으로 판단할 충돌체(콜라이더)
		[SerializeField] private Collider attackCollider;
		
		// [변수] 피해량의 배율 (100 = 100%)
		public int DamageMagnification { get; set; }
	}
}

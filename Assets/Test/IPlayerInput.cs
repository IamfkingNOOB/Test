namespace PlayerSystem_Ver2
{
	/// <summary>
	/// [인터페이스] 플레이어의 상태 클래스에서, 입력에 대한 처리를 정의합니다.
	/// </summary>
	internal interface IPlayerInput
	{
		internal void Move(UnityEngine.Vector2 inputVector);
		internal void Evade();
		internal void Attack();
		internal void ActivateWeaponSkill();
		internal void ActivateUltimate();
	}
}

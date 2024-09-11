namespace PlayerSystem
{
	/// <summary>
	/// [인터페이스] 플레이어의 상태 클래스에서, 컨트롤러가 전달하는 입력을 받아 행동을 구현합니다.
	/// </summary>
	internal interface IPlayerInputReceiver
	{
		internal void Move();
		internal void Evade();
		internal void Attack();
		internal void WeaponSkill();
		internal void Ultimate();
		internal void PetSkill();
	}
}

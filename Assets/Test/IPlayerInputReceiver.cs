namespace PlayerSystem_Ver2
{
	internal interface IPlayerInputReceiver
	{
		internal void Move(UnityEngine.Vector2 inputVector);
		internal void Evade();
		internal void Attack();
		internal void ActivateWeaponSkill();
		internal void ActivateUltimate();
	}
}

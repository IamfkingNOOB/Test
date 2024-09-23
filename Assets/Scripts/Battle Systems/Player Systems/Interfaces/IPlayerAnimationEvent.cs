namespace PlayerSystem
{
	internal interface IPlayerAnimationEvent
	{
		internal void StartIdleState();
		internal void StartInputStandby();
		internal void StartPreInputTime();
		internal void EndPreInputTime();
	}
}

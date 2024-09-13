namespace PlayerSystem
{
	/// <summary>
	/// [인터페이스] 플레이어의 상태 클래스에서, 상태 패턴을 구현합니다.
	/// </summary>
	internal interface IPlayerState
	{
		internal void Enter();
		internal void Execute();
		internal void Exit();
	}
}

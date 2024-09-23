namespace Framework.StatePattern
{
	/// <summary>
	/// [인터페이스] 상태 패턴을 구현합니다.
	/// </summary>
	internal interface IState
	{
		internal void Enter();
		internal void Execute();
		internal void Exit();
	}
}

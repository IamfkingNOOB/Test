namespace PlayerSystem
{
	// [클래스] 플레이어의 상태를 관리하는 최상위 클래스입니다.
	internal abstract class PlayerStateBase : IPlayerState, IPlayerInputReceiver
	{
		#region 변수(필드)

		// [Field] Player Controller
		protected readonly PlayerControllerBase Controller;
	
		#endregion 변수(필드)
	
		#region 함수(메서드)

		// [생성자] 변수를 초기화합니다.
		protected PlayerStateBase(PlayerControllerBase controller)
		{
			Controller = controller;
		}

		// [인터페이스 함수] IPlayerState 인터페이스
		void IPlayerState.Enter() => Enter();
		void IPlayerState.Execute() => Execute();
		void IPlayerState.Exit() => Exit();
	
		// [인터페이스 함수] IPlayerInput 인터페이스
		void IPlayerInputReceiver.Move() => Move();
		void IPlayerInputReceiver.Evade() => Evade();
		void IPlayerInputReceiver.Attack() => Attack();
		void IPlayerInputReceiver.WeaponSkill() => WeaponSkill();
		void IPlayerInputReceiver.Ultimate() => Ultimate();
		void IPlayerInputReceiver.PetSkill() => PetSkill();

		// [추상화 함수] IPlayerState 인터페이스 함수를 자식 클래스에서 재정의하기 위한 함수
		protected abstract void Enter();
		protected abstract void Execute();
		protected abstract void Exit();
	
		// [추상화 함수] IPlayerInput 인터페이스 함수를 자식 클래스에서 재정의하기 위한 함수
		protected abstract void Move();
		protected abstract void Evade();
		protected abstract void Attack();
		protected abstract void WeaponSkill();
		protected abstract void Ultimate();
		protected abstract void PetSkill();

		#endregion 함수(메서드)
	}
}

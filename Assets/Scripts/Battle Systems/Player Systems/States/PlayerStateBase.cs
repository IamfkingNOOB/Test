using Framework.StatePattern;

namespace PlayerSystem
{
	/// <summary>
	/// [클래스] 플레이어의 상태를 나타내는 클래스의 기본 구조를 정의합니다.
	/// </summary>
	internal abstract class PlayerStateBase : IState, IPlayerInput
	{
		#region 인터페이스(interface) 함수
		
		void IState.Enter() => Enter();
		void IState.Execute() => Execute();
		void IState.Exit() => Exit();

		void IPlayerInput.Move(UnityEngine.Vector2 inputVector) => Move(inputVector);
		void IPlayerInput.Evade() => Evade();
		void IPlayerInput.Attack() => Attack();
		void IPlayerInput.ActivateWeaponSkill() => ActivateWeaponSkill();
		void IPlayerInput.ActivateUltimate() => ActivateUltimate();

		#endregion 인터페이스(interface) 함수

		#region 추상화(abstract) 함수

		protected abstract void Enter();
		protected abstract void Execute();
		protected abstract void Exit();

		protected abstract void Move(UnityEngine.Vector2 inputVector);
		protected abstract void Evade();
		protected abstract void Attack();
		protected abstract void ActivateWeaponSkill();
		protected abstract void ActivateUltimate();

		#endregion 추상화(abstract) 함수
	}
}

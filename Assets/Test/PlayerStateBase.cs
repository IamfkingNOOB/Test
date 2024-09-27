using Framework.StatePattern;

namespace PlayerSystem_Ver2
{
	internal abstract class PlayerStateBase : IState, IPlayerInputReceiver
	{
		#region 인터페이스 함수
		
		void IState.Enter() => Enter();
		void IState.Execute() => Execute();
		void IState.Exit() => Exit();
	
		void IPlayerInputReceiver.Move(UnityEngine.Vector2 inputVector) => Move(inputVector);
		void IPlayerInputReceiver.Evade() => Evade();
		void IPlayerInputReceiver.Attack() => Attack();
		void IPlayerInputReceiver.ActivateWeaponSkill() => ActivateWeaponSkill();
		void IPlayerInputReceiver.ActivateUltimate() => ActivateUltimate();

		#endregion 인터페이스 함수
		
		#region 추상화 함수
		
		protected abstract void Enter();
		protected abstract void Execute();
		protected abstract void Exit();
	
		protected abstract void Move(UnityEngine.Vector2 inputVector);
		protected abstract void Evade();
		protected abstract void Attack();
		protected abstract void ActivateWeaponSkill();
		protected abstract void ActivateUltimate();
		
		#endregion 추상화 함수
	}
}

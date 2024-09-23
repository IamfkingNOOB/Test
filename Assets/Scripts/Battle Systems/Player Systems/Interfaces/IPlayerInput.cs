using UnityEngine.InputSystem;

namespace PlayerSystem
{
	/// <summary>
	/// [인터페이스] 플레이어의 컨트롤러 클래스에서, 상태 클래스에 입력에 대한 값을 전달합니다.
	/// </summary>
	internal interface IPlayerInput
	{
		public void OnMove(InputAction.CallbackContext context);
		public void OnEvade(InputAction.CallbackContext context);
		public void OnAttack(InputAction.CallbackContext context);
		public void OnWeaponSkill(InputAction.CallbackContext context);
		public void OnUltimate(InputAction.CallbackContext context);
		public void OnPetSkill(InputAction.CallbackContext context);
	}
}

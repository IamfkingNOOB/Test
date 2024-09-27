using UnityEngine;

namespace PlayerSystem_Ver2
{
	public class PlayerCombatController : MonoBehaviour
	{
		[SerializeField] private PlayerStateController stateController;
	
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out MonsterAttacker monsterAttacker))
			{
				stateController.ChangeState(new PlayerHitState(stateController, monsterAttacker));
			}
		}
	}
}

using PlayerSystem;
using UnityEngine;

public class IdleReseter : StateMachineBehaviour
{
    [SerializeField] private PlayerStateController _stateController;
    
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_stateController)
        {
            _stateController = animator.GetComponent<PlayerStateController>();
        }
        
        _stateController?.ChangeState(new PlayerIdleState(_stateController));
    }
    
    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if (!_stateController)
        {
            _stateController = animator.GetComponent<PlayerStateController>();
        }
        
        _stateController?.ChangeState(new PlayerIdleState(_stateController));
    }
}

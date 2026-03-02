using UnityEngine;

namespace HUnity.Components
{
    public class HUnityAnimStateMachineControllerComponent : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var animController = animator.gameObject.GetComponent<HUnityAnimControllerComponent>();
            animController?.FinishAnimation();
        }
    }
}
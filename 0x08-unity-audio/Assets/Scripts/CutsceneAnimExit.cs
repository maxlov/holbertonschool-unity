using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAnimExit : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject self = animator.gameObject;
        CutsceneController controller = self.GetComponent<CutsceneController>();
        controller.ExitCutscene();
    }
}

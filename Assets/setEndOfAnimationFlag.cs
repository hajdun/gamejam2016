using UnityEngine;
using System.Collections;

public class setEndOfAnimationFlag : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject cutSceneController = GameObject.Find("CutSceneController");
		cutsceneController cutsceneController = cutSceneController.GetComponent<cutsceneController> ();
		cutsceneController.animationFinished = true;
	}
}

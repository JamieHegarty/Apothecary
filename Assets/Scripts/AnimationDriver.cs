using UnityEngine;
using System.Collections;

public class AnimationDriver : MonoBehaviour {
	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
	}

	public void PlayWalkingAnimation (bool state){
		animator.SetBool ("IsWalking", state);
	}

}

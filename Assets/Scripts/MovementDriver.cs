using UnityEngine;
using System.Collections;

public class MovementDriver : MonoBehaviour {

	private AnimationDriver animationDriver;
	private UnityEngine.AI.NavMeshAgent navMeshAgent;

	private bool playerIsWalking;
	private bool walkingStateLastTick;
	private bool destinationIsObject;
	private float stoppingDistance = 0.0f;

	public delegate void ReachedObjectCallback ();
	public static event ReachedObjectCallback reachedObjectEvent;


	void Awake () {
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		animationDriver = GetComponent<AnimationDriver> ();
	}

	void Update () {
		CheckMovementStatus ();
		UpdateAnimationDriver ();
	}

	private void CheckMovementStatus(){
		if (navMeshAgent.hasPath) {
			if (navMeshAgent.remainingDistance <= stoppingDistance) {
				navMeshAgent.Stop ();
				navMeshAgent.ResetPath ();

				//Callback to PlayerInteraction to tell it we are at the object
				if (destinationIsObject) {
					if (reachedObjectEvent != null) {
						reachedObjectEvent ();
					}
				}

			}
		}
	}

	private void UpdateAnimationDriver (){
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon) {
				playerIsWalking = false;
			}
		} else  {
			playerIsWalking = true;
		}

		if (playerIsWalking != walkingStateLastTick) {
			animationDriver.PlayWalkingAnimation (playerIsWalking);
			walkingStateLastTick = playerIsWalking;
		}
	}

	public void UpdateDestination (Vector3 position, bool isAnObject=false){
		navMeshAgent.destination = position;
		destinationIsObject = isAnObject;
		navMeshAgent.Resume ();
	}

	public void SetPlayerWalking (bool playerWalkingState){
		playerIsWalking = playerWalkingState;
	}

	public bool CheckPlayerHasPath (){
		return navMeshAgent.hasPath;
	}

	public void ForceNavMeshAgentResume (){
		navMeshAgent.Resume ();
	}

	public void ForceNavMeshAgentStop (){
		navMeshAgent.Stop ();
	}

	public void SetStoppingDistance (float dist){
		stoppingDistance = dist;
		Debug.Log ("Stopping distance set to"+stoppingDistance);

	}
}

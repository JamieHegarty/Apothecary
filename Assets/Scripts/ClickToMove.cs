using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

	/*
	 * NOTE: On second thought could probably make interaction distance object specific, and pull
	 * the value when you click the object, that way objects of different sizes behave differently
	*/
	public float interactionDistance = 1.0f; //Prob needs to be made private or we will forget to update in the editor

	public PlayerInteraction interactionScript; //Not sure should this be manually assigned or do we Get Component in the awake function

	private Animator anim;
	private NavMeshAgent navMeshAgent;
	private GameObject targetedObject;

	private bool playerIsWalking;
	private bool objectClicked;

	/*This is the distance from the camera to the floor of the scene 
	  Choose an arbitraily large number so that it always reaches */
	private float mouseRaycastDistance = 100.0f;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		interactionScript = GetComponent<PlayerInteraction> ();
	}
	
	// Update is called once per frame
	void Update () {
		HandlePlayerInput ();

		if (objectClicked && navMeshAgent.hasPath){
			MoveToObjectAndInteract ();
		}

		PlayerAnimationManager ();
	}

	private void HandlePlayerInput() {
		Ray mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseRaycastHit;

		if (Input.GetButtonDown ("Fire1")) { //Default "Fire1" is Mouse 0
			if(Physics.Raycast(mouseRaycast, out mouseRaycastHit, mouseRaycastDistance)){

				if (mouseRaycastHit.collider.CompareTag ("InteractableObject")){
					targetedObject = mouseRaycastHit.collider.gameObject;
					navMeshAgent.destination = targetedObject.transform.position;
					objectClicked = true;
				} else {
					playerIsWalking = true;
					objectClicked = false;
					navMeshAgent.destination = mouseRaycastHit.point;
					navMeshAgent.Resume ();
				}
			}
		}
	}

	/*
	TODO: Make direction changing sync up with animations better
		  e.g. we can either not move when changing direction (like runescape)
			   or we can have turning animations (needs investigation)
	*/
	private void MoveToObjectAndInteract() {
		if(targetedObject == null){
			return;
		}

		navMeshAgent.destination = targetedObject.transform.position;

		if (navMeshAgent.remainingDistance > interactionDistance) {
			//If we are still not in range, continute to walk toward the object
			navMeshAgent.Resume ();
			playerIsWalking = true;
		} else if (navMeshAgent.remainingDistance <= interactionDistance){
			//In range, do some stuff
			Vector3 lookPosition = targetedObject.transform.position;
			lookPosition.y = 0;
			transform.LookAt (lookPosition);
			navMeshAgent.Stop ();
			playerIsWalking = false;
			interactionScript.HandleObject (targetedObject);
			ResetPlayerClick ();
		}
	}

	private void PlayerAnimationManager() {
		if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
			if (!navMeshAgent.hasPath || Mathf.Abs (navMeshAgent.velocity.sqrMagnitude) < float.Epsilon) {
				playerIsWalking = false;
			}
		} else {
			playerIsWalking = true;
		}

		Debug.Log ("IsWalking: " + playerIsWalking);
		anim.SetBool ("IsWalking", playerIsWalking);
	}

	private void ResetPlayerClick(){
		objectClicked = false;
		targetedObject = null;
		navMeshAgent.ResetPath ();
	}
}

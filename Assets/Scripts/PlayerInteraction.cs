using UnityEngine;
using System.Collections;

/* 
 * This could perhaps be the main player class
 * Providing access to inventory and other such things
 * Work in progress
*/
public class PlayerInteraction : MonoBehaviour {

	//TODO: Remove
	private PlayerMovementController playerMovementDriver;
	private AnimationDriver animationDriver;

    private bool actingOnObject; //Represents if we are doing something regarding an object, open to better name suggestions
	GameObject selectedObject;

	void Awake() {
		//TODO: Remove
		playerMovementDriver = GetComponent<PlayerMovementController> ();
    }

	private void Start() {
		GlobalEventHandler.Instance.ObjectClickedEvent += ProcessObject;
	}

	void ProcessObject(GameObject obj){
		Debug.Log("Process Object: " + obj.name);
		InteractableObject interactableObject = obj.GetComponent<InteractableObject>();
		interactableObject.DefaultAction();

	}
	
	// -----------------------------------------------------------------------------

	public void ClickedNonObject(Vector3 targetDestination){
		playerMovementDriver.SetPlayerWalking (true);
		playerMovementDriver.UpdateDestination (targetDestination);
		playerMovementDriver.SetStoppingDistance (0.0f);
		actingOnObject = false;
	}

	public void ClickedInteractableObject(GameObject objectClicked){
		selectedObject = objectClicked;
		InteractableObject objectScript = objectClicked.GetComponent<InteractableObject> ();

		playerMovementDriver.UpdateDestination (selectedObject.transform.position, true);
		playerMovementDriver.SetStoppingDistance (objectScript.interactionDistance);

		/* In this scenario we are subscribing the UseObject function to the event in Movement Driver
		   so when it reaches the object it will trigger a call to this function.
		   This should be interfaced further to allow us to add different function calls but for 
		   now consider it proof of concept */
		PlayerMovementController.reachedObjectEvent += UseObject;

		actingOnObject = true;
	}

	void UseObject () {
		Debug.Log ("Using object");
		InteractableObject objectScript = selectedObject.GetComponent<InteractableObject> ();
		objectScript.PlayerUseObject ();

		PlayerMovementController.reachedObjectEvent -= UseObject; //Remove the callback from the event list as it has been used

	}
}
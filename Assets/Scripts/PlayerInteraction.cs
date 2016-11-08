using UnityEngine;
using System.Collections;

/* 
 * This could perhaps be the main player class
 * Providing access to inventory and other such things
 * Work in progress
*/
public class PlayerInteraction : MonoBehaviour {

	/* TODO: refactor function to decide what to do with the object
	   passed in and call the appropriate local function */
	public void HandleObject(GameObject gameObj) {
		Debug.Log ("Using object");
		InteractableObject obj = gameObj.GetComponent<InteractableObject> ();
		obj.PlayerUseObject ();
	}
}

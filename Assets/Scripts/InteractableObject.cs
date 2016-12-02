using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	public string objectName = "";
	public float interactionDistance = 1.5f;

	public void PlayerUseObject (){
		Debug.Log ("Picked up " + objectName);
		CleanUpObject ();
	}

	private void CleanUpObject (){
		Destroy (gameObject);
	}
}

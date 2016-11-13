using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	public string objectName = "";

	public void PlayerUseObject(){
		Debug.Log ("Picked up " + objectName);
		CleanUpObject ();
	}

	private void CleanUpObject(){
		Destroy (gameObject);
	}
}

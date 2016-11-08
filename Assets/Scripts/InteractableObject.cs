using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	public string name = "";

	public void PlayerUseObject(){
		Debug.Log ("Picked up " + name);
		CleanUpObject ();
	}

	private void CleanUpObject(){
		Destroy (gameObject);
	}
}

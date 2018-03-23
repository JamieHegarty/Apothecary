using UnityEngine;
using System.Collections;

public abstract class InteractableObject : MonoBehaviour {

	/* Override these in child classes if needs be */
	public string objectName = "";
	public float interactionDistance = 1.5f;

	public abstract void DefaultAction();

	public void Use()
	{
		Debug.Log("Use");
	}
	
	public void Walk()
	{
		Debug.Log("Walk");
	}
	
	public void Open()
	{
		Debug.Log("Open");
	}
	
	public void Drop()
	{
		Debug.Log("Drop");
	}
	
	public void Examine()
	{
		Debug.Log("Examine");
	}
	
	public void Take()
	{
		Debug.Log("Take");
	}
	
	// ---------------------------------------------

	public void PlayerUseObject (){
		Debug.Log ("Picked up " + objectName);
		CleanUpObject ();
	}

	private void CleanUpObject (){
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class PlayerInputManager : MonoBehaviour {
	
	private PlayerInteraction playerInteraction;

	/*This is the distance from the camera to the floor of the scene
	  Choose an arbitraily large number so that it always reaches */
	private float mouseRaycastDistance = 100.0f;

	void Awake () {
		playerInteraction = GetComponent<PlayerInteraction> ();
	}

	void Update () {
		HandlePlayerInput ();
	}

	private void HandlePlayerInput() {
		Ray mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseRaycastHit;

		if(Physics.Raycast(mouseRaycast, out mouseRaycastHit, mouseRaycastDistance)){ //This makes sure the mouse is actually over an object, otherwise we don't care
			if (Input.GetButtonDown ("Fire1")) {
				if (mouseRaycastHit.collider.CompareTag ("InteractableObject")){
					playerInteraction.ClickedInteractableObject (mouseRaycastHit.collider.gameObject);
				} else {
					playerInteraction.ClickedNonObject (mouseRaycastHit.point);
				}

			} else if (Input.GetButtonDown ("Fire2")){
				//Call context menu stuff here
			}
		}
	}
}

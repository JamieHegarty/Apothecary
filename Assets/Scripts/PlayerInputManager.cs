using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour {
	
	private PlayerInteraction playerInteraction;
    private ContextMenu contextMenu;

    /*This is the distance from the camera to the floor of the scene
	  Choose an arbitraily large number so that it always reaches */
    private float mouseRaycastDistance = 100.0f;

	void Awake () {
		playerInteraction = GetComponent<PlayerInteraction> ();
        contextMenu = GetComponent<ContextMenu>();
    }

	void Update () {
		HandlePlayerInput ();
	}

	private void HandlePlayerInput() {
		Ray mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseRaycastHit;

		if(Physics.Raycast(mouseRaycast, out mouseRaycastHit, mouseRaycastDistance)) { //This makes sure the mouse is actually over an object, otherwise we don't care
			if (Input.GetButtonDown ("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {
				if (mouseRaycastHit.collider.CompareTag ("InteractableObject")){
					playerInteraction.ClickedInteractableObject (mouseRaycastHit.collider.gameObject);
				} else {
					playerInteraction.ClickedNonObject (mouseRaycastHit.point);
				}

			} else if (Input.GetButtonDown ("Fire2")) {                
                if (mouseRaycastHit.collider.CompareTag("InteractableObject")) {
                    contextMenu.objectClicked = mouseRaycastHit.collider.gameObject;
                    contextMenu.TestC("Use");                   
                } else {
                    contextMenu.walkLocation = mouseRaycastHit.point;
                    contextMenu.TestC("Walk");   
                }

            }
		}
	}
}

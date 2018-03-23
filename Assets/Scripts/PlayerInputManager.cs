using UnityEngine;
using System.Collections;
using DefaultNamespace;
using UnityEngine.EventSystems;

public class PlayerInputManager : MonoBehaviour {
	
	private PlayerInteraction playerInteraction;
    private ContextMenu contextMenu;

    /*This is the distance from the camera to the floor of the scene
	  Choose an arbitraily large number so that it always reaches */
    private float mouseRaycastDistance = 100.0f;
	
	private Ray mouseRaycast;
	private RaycastHit mouseRaycastHit;

	void Awake () {
		//TODO: Remove
		playerInteraction = GetComponent<PlayerInteraction> ();
        contextMenu = GetComponent<ContextMenu>();
    }

	void HandleLeftClick(GameObject obj, Vector3 mousePosition) {
		Debug.Log("HandleLeftClick - Clicked: " + obj.name);
		
		/*
		  If we don't click an interactable object we create and empty object,
		  assign the ground object script to it and apply the mouse transform 
		  so that we can move towards that point.
		*/
		if (obj.CompareTag("Ground")) //TODO: We might need to remove this ground tag I don't really like it - Jamie
		{
			obj = new GameObject("tempGroundObject");
			obj.transform.position = mousePosition;
			obj.AddComponent<Ground>();
			GlobalEventHandler.Instance.ObjectClicked(obj);

			return;
		}
		
		GlobalEventHandler.Instance.ObjectClicked(obj);
	}

	void HandleRightClick(GameObject obj) {
		Debug.Log("HandleRightClick - Clicked: " + obj.name);
	}
	/** ----------------------------- OLD CODE \/ -------------------------- **/

	void ListenForInputs() {
		mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(mouseRaycast, out mouseRaycastHit, mouseRaycastDistance)) { //This makes sure the mouse is actually over an object, otherwise we don't care
			if (Input.GetButtonDown ("Fire1")) {
				HandleLeftClick(mouseRaycastHit.collider.gameObject, mouseRaycastHit.point);
			} else if (Input.GetButtonDown ("Fire2")) {   
				HandleRightClick(mouseRaycastHit.collider.gameObject);

				/*if (mouseRaycastHit.collider.CompareTag("InteractableObject")) {
					contextMenu.objectClicked = mouseRaycastHit.collider.gameObject;
					contextMenu.TestC("Use");                   
				} else {
					contextMenu.walkLocation = mouseRaycastHit.point;
					contextMenu.TestC("Walk");   
				}
				*/
			}
		}
	}
	
	void Update () {
		//HandlePlayerInput ();
		ListenForInputs();
	}

	private void HandlePlayerInput() {
		Ray mouseRaycast = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit mouseRaycastHit;

		if(Physics.Raycast(mouseRaycast, out mouseRaycastHit, mouseRaycastDistance)) { //This makes sure the mouse is actually over an object, otherwise we don't care
			if (Input.GetButtonDown ("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {
				contextMenu.modalPanel.modalPanelObject.SetActive(false);
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

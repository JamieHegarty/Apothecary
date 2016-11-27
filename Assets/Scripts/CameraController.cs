using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform playerPositionReference;

	private float xCameraPosition = 0.0f;
	private float yCameraPosition = 0.0f;
	private float xMouseSpeedModifier = 5.0f;
	private float yMouseSpeedModifier = 3.0f;
	private float xArrowKeySpeedModifier = 1.2f;
	private float yArrowKeySpeedModifier = 1.2f;
	private float minYRotation = 20;
	private float maxYRotaion = 70;

	private int zoomRate = 40;
	private float maxViewDistance = 12;
	private float minViewDistance = 3;
	private float cameraCollisionHeight = 0.5f;
	private float desiredDistance;
	private float correctedDistance;
	private float currentDistance;

	void Start () {
		Vector3 angles = transform.eulerAngles;
		xCameraPosition = angles.x;
		yCameraPosition = angles.y + 30f;

		desiredDistance = 5;
		currentDistance = desiredDistance;
		correctedDistance = desiredDistance;
	}

	void LateUpdate () {
		HandleUserInput ();
		Quaternion cameraRotation = CalculateCameraRotation ();
		Vector3 cameraPosition = CalculateCameraPosition (cameraRotation);
		CheckForCameraCollisions (ref cameraPosition, cameraRotation);

		transform.rotation = cameraRotation;
		transform.position = cameraPosition;
	}

	private void HandleUserInput() {
		if (Input.GetMouseButton (2)) { //2 == middle mouse button
			xCameraPosition += Input.GetAxis ("Mouse X") * xMouseSpeedModifier;
			yCameraPosition -= Input.GetAxis ("Mouse Y") * yMouseSpeedModifier;
		}

		if (Input.GetKey(KeyCode.LeftArrow)){
			xCameraPosition += xArrowKeySpeedModifier;
		}

		if (Input.GetKey(KeyCode.RightArrow)){
			xCameraPosition -= xArrowKeySpeedModifier;
		}

		if (Input.GetKey(KeyCode.UpArrow)){
			yCameraPosition += yArrowKeySpeedModifier;
		}

		if (Input.GetKey(KeyCode.DownArrow)){
			yCameraPosition -= yArrowKeySpeedModifier;
		}
	}

	private Quaternion CalculateCameraRotation (){
		yCameraPosition = ClampAngle (yCameraPosition, minYRotation, maxYRotaion);

		return  Quaternion.Euler (yCameraPosition, xCameraPosition, 0);
	}


	private Vector3 CalculateCameraPosition (Quaternion cameraRotation){
		desiredDistance -= Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs (desiredDistance);
		correctedDistance = Mathf.Clamp (desiredDistance, minViewDistance, maxViewDistance);

		return playerPositionReference.position - (cameraRotation * Vector3.forward * correctedDistance);
	}

	private void CheckForCameraCollisions(ref Vector3 cameraPosition, Quaternion cameraRotation) {
		RaycastHit cameraCollisionHit;
		Vector3 cameraTargetPosition = new Vector3 (
			playerPositionReference.position.x,
			playerPositionReference.position.y + cameraCollisionHeight, 
			playerPositionReference.position.z
		);

		bool isCorrected = false;

		if (Physics.Linecast (cameraTargetPosition, cameraPosition, out cameraCollisionHit)) {
			cameraPosition = cameraCollisionHit.point;
			correctedDistance = Vector3.Distance (cameraTargetPosition, cameraPosition);
			isCorrected = true;
		}

		if (!isCorrected || (correctedDistance > currentDistance)) {
			currentDistance = Mathf.Lerp (currentDistance, correctedDistance, Time.deltaTime * zoomRate);
		} else {
			currentDistance = correctedDistance;
		}

		cameraPosition = playerPositionReference.position - (cameraRotation * Vector3.forward * currentDistance + new Vector3 (0, -cameraCollisionHeight, 0));
	}

	private static float ClampAngle (float angle, float min, float max) {
		if (angle < -360) {
			angle += 360;
		}

		if (angle > 360) {
			angle -= 360;
		}

		return Mathf.Clamp (angle, min, max);
	}
}

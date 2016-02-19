using UnityEngine;
using System.Collections;

public class VRWalk : MonoBehaviour {

	public float movementSpeed = 400f;
	public float mouseSensitivity = 7f;
	public float runningSpeedFactor = 2f;
	public float verticalAngleLimit = 85f;

	float verticalAngle = 0;
	Touch myTouch = new Touch();

	bool fingerOnScreen = false;
	GameObject head;

	private float leftright(){
		//By Mouse
		return Input.GetAxis ("Mouse X");
	}

	private float updown(){
		//By Mouse
		return Input.GetAxis ("Mouse Y");
	}

	private void touchDeviceStepForward(){
		if (Input.touchCount > 0) {
			Touch myTouch = Input.GetTouch (0);
			if (myTouch.phase == TouchPhase.Began || myTouch.phase == TouchPhase.Stationary) {
				Vector3 speed = GameObject.Find ("CardboardMain/Head").transform.forward;
				CharacterController controller = GetComponent<CharacterController> ();
				controller.SimpleMove (speed);
			}
		}
	}

	//Change this function if you want to modify Mouse Input
	private void mouseLookAndPointer(){

		transform.Rotate (0, leftright () * mouseSensitivity, 0);

		float updownVar = updown () * mouseSensitivity;

		verticalAngle -= updownVar;
		float finalVerticalRotation = Mathf.Clamp(verticalAngle,-verticalAngleLimit,verticalAngleLimit);

		Camera.main.transform.localRotation = Quaternion.Euler(finalVerticalRotation, 0, 0);

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		touchDeviceStepForward ();
		mouseLookAndPointer ();
	}
}

using UnityEngine;
using System.Collections;

public class walkBasicInputs : MonoBehaviour {

	public float movementSpeed = 10f;
	public float mouseSensitivity = 7f;
	public float runningSpeedFactor = 2f;
	public float verticalAngleLimit = 85f;

	public bool MouseInputActivated = true;
	public bool KeyboardInputActivated = true;

	float verticalAngle = 0;

	//Interface encapsulation

	private float leftright(){
		//By Mouse
		return Input.GetAxis ("Mouse X");
	}

	private float updown(){
		//By Mouse
		return Input.GetAxis ("Mouse Y");
	}

	private float forward(){
		//By Keyboard
		return Input.GetAxis ("Vertical");
	}

	private float sideways(){
		//By Keyboard
		return Input.GetAxis ("Horizontal");
	}

	private bool runningButton(){
		//By Keyboard, left shift
		return Input.GetKey("left shift");
	}

	//Private functions for input modules


	//Change this function if you want to modify Keyboard Input
	private void keyboardController(){

		float running = 1f;
		if (runningButton ()) {
			running = runningSpeedFactor;
		} else {
			running = 1f;
		}

		Vector3 speed = new Vector3 (sideways()*movementSpeed, 0, forward()*movementSpeed*running);

		//Makes the rotation while using mouse
		speed = transform.rotation * speed;

		CharacterController controller = GetComponent<CharacterController> ();
		controller.SimpleMove (speed);
	}

	//Change this function if you want to modify Mouse Input
	private void mouseLookAndPointer(){

		transform.Rotate (0, leftright () * mouseSensitivity, 0);

		float updownVar = updown () * mouseSensitivity;

		verticalAngle -= updownVar;
		float finalVerticalRotation = Mathf.Clamp(verticalAngle,-verticalAngleLimit,verticalAngleLimit);

		Camera.main.transform.localRotation = Quaternion.Euler(finalVerticalRotation, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (KeyboardInputActivated)
			keyboardController ();
		if (MouseInputActivated)
			mouseLookAndPointer ();
	}
}

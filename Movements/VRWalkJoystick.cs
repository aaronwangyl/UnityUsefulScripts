using UnityEngine;
using System.Collections;

public class VRWalkJoystick : MonoBehaviour {

	//This script was created to be used with Cardboard Interface

	//Important: Make sure you deactivate the "Tap is Trigger" option
	//To do that, select your Cardboard Object (i.e CarboardMain) and at the inspector unselect "Tap is Trigger" Option located in "Cardboard Settings"

	//This version of the script can be used whether there's a joystick or not. Choose from the script component the option named "Touch to move"
	//in order to switch between these modes.


	public float movementSpeed = 10f;
	public float mouseSensitivity = 7f;
	public bool touchToMove = false; 

	private float forward(){
		//By Keyboard
		return Input.GetAxis ("Vertical");
	}

	private float sideways(){
		//By Keyboard
		return Input.GetAxis ("Horizontal");
	}

	////Private functions for input modules

	private void touchToGoForward(){
		if (Input.touchCount > 0) {
			Touch myTouch = Input.GetTouch (0);
			if (myTouch.phase == TouchPhase.Began || myTouch.phase == TouchPhase.Stationary) {
				Vector3 speed = GameObject.Find ("CardboardMain/Head").transform.forward;
				CharacterController controller = GetComponent<CharacterController> ();
				controller.SimpleMove (speed);
			}
		}
	}

	private void joystickForward(){
		Vector3 speed = new Vector3 (sideways()*movementSpeed, 0, forward()*movementSpeed);

		//Makes the rotation while using Cardboard Head
		speed = GameObject.Find ("CardboardMain/Head").transform.rotation * speed;

		CharacterController controller = GetComponent<CharacterController> ();
		controller.SimpleMove (speed);
	}

	// Update is called once per frame
	void Update () {
		if (touchToMove) {
			touchToGoForward ();
		} else {
			joystickForward ();
		}
	}
}

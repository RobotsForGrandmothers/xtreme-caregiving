using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Elevator))]
public class ElevatorControl : MonoBehaviour {
	public float speed = 5.0f;
	private Elevator elevator;

	// Use this for initialization
	void Start () {
		elevator = this.GetComponent<Elevator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (elevator.GetElevatorFloor ().doorLeft.IsOpen ()) {
				elevator.CloseLeftDoor ();
			} else {
				elevator.OpenLeftDoor ();
			}
		} 
		if (Input.GetMouseButtonDown (1)) {
			if (elevator.GetElevatorFloor ().doorRight.IsOpen ()) {
				elevator.CloseRightDoor ();
			} else {
				elevator.OpenRightDoor ();
			}
		}
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			Vector2 localPosition = elevator.transform.localPosition;
			localPosition.y += speed * Input.GetAxis ("Mouse ScrollWheel");
			localPosition.y = Mathf.Clamp(localPosition.y, elevator.minHeight, elevator.maxHeight);
			elevator.transform.localPosition = localPosition;
		}
		
	}
}

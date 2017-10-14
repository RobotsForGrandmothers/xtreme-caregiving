using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour {
	public Elevator elevator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (elevator.GetElevatorFloor().left.IsOpen()) {
				elevator.CloseLeftDoor ();
			} else {
				elevator.OpenLeftDoor ();
			}
		}
		else if (Input.GetMouseButtonDown(1)){
			if (elevator.GetElevatorFloor().right.IsOpen()) {
				elevator.CloseRightDoor ();
			} else {
				elevator.OpenRightDoor ();
			}
		}

		
	}
}

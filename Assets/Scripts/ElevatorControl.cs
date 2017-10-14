using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControl : MonoBehaviour {
	public Elevator elevator;
	public int maxHight;
	public int minHight;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (elevator.GetElevatorFloor ().left.IsOpen ()) {
				elevator.CloseLeftDoor ();
			} else {
				elevator.OpenLeftDoor ();
			}
		} else if (Input.GetMouseButtonDown (1)) {
			if (elevator.GetElevatorFloor ().right.IsOpen ()) {
				elevator.CloseRightDoor ();
			} else {
				elevator.OpenRightDoor ();
			}
		} else if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			Vector2 position = elevator.transform.position;
			position.y += Input.GetAxis ("Mouse ScrollWheel");
			elevator.transform.position = position;
		}
		
	}
}

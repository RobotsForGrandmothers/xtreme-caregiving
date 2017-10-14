using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : MonoBehaviour {
	public int startPositionX;
	public int startPositionY;
	public int numNodesInElevator;

	private Floor floor;
	private int[] position;
	private Node[] nodes;

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Floor>() != null) {
			floor = other.GetComponent<Floor> ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Floor>() == floor){
			floor = null;
		}
	}
	void ConnectNode(){
		
	}
	void DisConnectNode(){

	}

	void CloseLeftDoor(){
		floor.left.Close ();

	}
	void OpenLeftDoor(){
		floor.left.Open ();
	}
	void CloseRightDoor(){
		floor.right.Close ();
	}
	void OpenRightDoor(){
		floor.right.Open ();
	}

	// Use this for initialization
	void Start () {
		position = new int[2];
		position [0] = startPositionX;
		position [1] = startPositionY;

		nodes = new Node[numNodesInElevator];

	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (floor.left.IsOpen()) {
				CloseLeftDoor ();
			} else {
				OpenLeftDoor ();
			}
		}
		else if (Input.GetMouseButtonDown(1)){
			if (floor.right.IsOpen()) {
				CloseRightDoor ();
			} else {
				OpenRightDoor ();
			}
		}

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Elevator : MonoBehaviour {
	public int numNodesInElevator;

	private Floor floor;
	private Node[] nodes;

	public Floor GetElevatorFloor(){
		return floor;
	}

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
	void ConnectNode(bool Left){
		//must be called from one of the close door functions
		if (Left) {
			Node leftE = nodes [0];
			Node leftFIn = floor.GetEntranceLeft ();
			Node leftFOut = floor.GetExitLeft ();

			leftE.left = leftFOut;
			leftFIn.right = leftE;
		} else {
			Node rightE = nodes [(nodes.Length - 1)];
			Node rightFIn = floor.GetEntranceRight ();
			Node rightFOut = floor.GetExitRight ();

			rightE.right = rightFOut;
			rightFIn.left = rightE;
		}

	}
	void DisConnectNode(bool Left){
		//must be called from one of the close door functions
		if (Left) {
			Node leftE = nodes [0];
			Node leftFIn = floor.GetEntranceLeft ();

			leftE.left = null;
			leftFIn.right = null;
		} else {
			Node rightE = nodes [(nodes.Length - 1)];
			Node rightFIn = floor.GetEntranceRight ();

			rightE.right = null;
			rightFIn.left = null;
		}

	}

	public void CloseLeftDoor(){
		if (floor != null) {
			floor.left.Close ();
			DisConnectNode (true);
		} else {
			throw new Exception ("Cannot close floor door because elevator is not at a floor");
		}
	}
	public void OpenLeftDoor(){
		if (floor != null) {
			floor.left.Open ();
			ConnectNode (true);
		} else {
			throw new Exception ("Cannot close floor door because elevator is not at a floor");
		}
	}
	public void CloseRightDoor(){
		if (floor != null) {
			floor.right.Close ();
			DisConnectNode (false);
		} else {
			throw new Exception ("Cannot close floor door because elevator is not at a floor");
		}
	}
	public void OpenRightDoor(){
		if (floor != null) {
			floor.right.Open ();
			ConnectNode (false);
		} else {
			throw new Exception ("Cannot close floor door because elevator is not at a floor");
		}
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

	}
}

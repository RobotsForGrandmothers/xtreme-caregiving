using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Elevator : MonoBehaviour {
	int _numNodesInElevator;
	public int numNodesInElevator {
		get { return _numNodesInElevator; }
		set { _numNodesInElevator = value; RecreateNodes (); }
	}
	float _nodeSpacing = 1;
	public float nodeSpacing {
		get { return _nodeSpacing; }
		set { _nodeSpacing = value; RepositionNodes (); }
	}

	public float maxHeight;
	public float minHeight;

	private Floor _floor;
	private Floor floor {
		get { return _floor; }
		set { 
			if (_floor != null) {
				DisConnectNode (true);
				DisConnectNode (false);
			}
			_floor = value;
		}
	}
	private Node[] nodes;

	public Floor GetElevatorFloor(){
		return floor;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Floor>() != null) {
			floor = other.GetComponent<Floor> ();
			Debug.Log ("Elevator reached floor " + floor);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.GetComponent<Floor>() == floor){
			Debug.Log ("Elevator left floor " + floor);
			floor = null;
		}
	}
	void ConnectNode(bool Left){
		//must be called from one of the close door functions
		if (Left) {
			Node leftE = nodes [0];
			Node leftFIn = floor.GetEntranceLeft ();
			Node leftFOut = floor.GetExitLeft ();

			leftE.left = leftFIn;
			leftFOut.right = leftE;
		} else {
			Node rightE = nodes [(nodes.Length - 1)];
			Node rightFIn = floor.GetEntranceRight ();
			Node rightFOut = floor.GetExitRight ();

			rightE.right = rightFIn;
			rightFOut.left = rightE;
		}

		Debug.Log ("Connected floor nodes on " + (Left ? "left" : "right") + " side");
	}
	void DisConnectNode(bool Left){
		//must be called from one of the close door functions
		if (Left) {
			Node leftE = nodes [0];
			Node leftFOut = floor.GetExitLeft ();

			leftE.left = null;
			leftFOut.right = null;
		} else {
			Node rightE = nodes [(nodes.Length - 1)];
			Node rightFOut = floor.GetExitRight ();

			rightE.right = null;
			rightFOut.left = null;
		}
		
		Debug.Log ("Disconnected floor nodes on " + (Left ? "left" : "right") + " side");
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
		RecreateNodes ();
	}

	void RecreateNodes() {
		// delete old nodes
		if (nodes != null) {
			foreach (Node node in nodes) {
				Destroy (node.gameObject);
			}
		}

		nodes = new Node[numNodesInElevator];

		// create nodes going into each side of the floor
		for (int i = 0; i < numNodesInElevator; ++i) {
			nodes [i] = CreateNode("Node " + i);

			if (i > 0) {
				nodes [i - 1].right = nodes [i];
				nodes [i].left = nodes [i - 1];
			}
		}

		// reposition new nodes
		RepositionNodes ();
	}

	void RepositionNodes() {
		for (int i = 0; i < numNodesInElevator; ++i) {
			nodes [i].transform.transform.localPosition = new Vector2 ((i - (float)(numNodesInElevator - 1) / 2) * nodeSpacing, 0);
		}
	}

	Node CreateNode(string name) {
		GameObject node = new GameObject(name);
		node.transform.SetParent (this.transform);
		node.AddComponent<Node>();
		node.GetComponent<Node> ().inElevator = true;
		return node.GetComponent<Node> ();
	}

	// Update is called once per frame
	void Update () {

	}
}

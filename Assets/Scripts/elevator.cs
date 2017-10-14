using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
	public int startPositionX;
	public int startPositionY;
	public int numNodesInElevator;
	private int[] position;
	private Node[] nodes;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour {
	public int startPositionX;
	public int startPositionY;
	public int numNodesInElevator;
	private int[] position;
	private Node[] nodes;
	private Door left;
	private Door right;

	// Use this for initialization
	void Start () {
		position = new int[2];
		position [0] = startPositionX;
		position [1] = startPositionY;

		nodes = new Node[numNodesInElevator];
		left = new Door ();
		right = new Door ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PersonSpawner))]
public class Building : MonoBehaviour {
	public int floors = 0;
	public int floorLength = 5;
	public int nodeSpacing = 1;
	public int elevatorWidth = 5;
	public float floorHeight = 2;
	private Floor[] floorArray;
	public GameObject floorPrefab;
	public GameObject elevatorPrefab;

	// Use this for initialization
	void Start () {
		// create floors
		floorArray = new Floor[floors];
		for (int i = 0; i < floors; ++i) {
			GameObject obj = Instantiate (floorPrefab, this.transform);
			obj.name = "Floor " + i;
			obj.transform.localPosition = new Vector2 (0, floorHeight * i);
			Floor floor = obj.GetComponent<Floor> ();
			floor.halfLength = floorLength;
			floor.middleWidth = elevatorWidth;
			floor.nodeSpacing = nodeSpacing;

			floorArray [i] = floor;
		}

		this.GetComponent<PersonSpawner> ().nodeGetter = GetRandomNode;
	}

	Node GetRandomNode() {
		System.Random rand = new System.Random();
		Floor floor = floorArray[rand.Next(floorArray.Length)];
		bool left = rand.Next (2) == 0;
		Node[] nodes;
		if (left) {
			nodes = floor.GetOutNodesLeft ();
		} else {
			nodes = floor.GetOutNodesRight ();
		}
		return nodes [rand.Next (nodes.Length)];
	}
}

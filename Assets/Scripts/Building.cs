using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PersonSpawner))]
[RequireComponent(typeof(BoxCollider2D))]
public class Building : MonoBehaviour {
	public int floors = 0;
	public int floorLength = 5;
	public int nodeSpacing = 1;
	public int elevatorWidth = 5;
	public float floorHeight = 2;
	private Floor[] floorArray;
	private Elevator elevator;
	public Floor floorPrefab;
    public Floor dinerPrefab;
	public Elevator elevatorPrefab;
	System.Random rand = new System.Random ((int)(0xcea9e245 ^ (int)System.DateTime.Now.TimeOfDay.TotalMilliseconds)); // because it works

	// Use this for initialization
	void Start () {
		// create floors
		floorArray = new Floor[floors];
        GameObject obj = Instantiate(dinerPrefab.gameObject, this.transform);
        obj.name = "Diner";
        obj.transform.localPosition = new Vector2(0, floorHeight * 0);
        Floor floor = obj.GetComponent<Floor>();
        floor.halfLength = floorLength;
        floor.middleWidth = elevatorWidth;
        floor.nodeSpacing = nodeSpacing;

        floorArray[0] = floor;
        for (int i = 1; i < floors; ++i) {
			obj = Instantiate (floorPrefab.gameObject, this.transform);
			obj.name = "Floor " + i;
			obj.transform.localPosition = new Vector2 (0, floorHeight * i);
			floor = obj.GetComponent<Floor> ();
			floor.halfLength = floorLength;
			floor.middleWidth = elevatorWidth;
			floor.nodeSpacing = nodeSpacing;

			floorArray [i] = floor;
		}

		// set node spawn function
		this.GetComponent<PersonSpawner> ().nodeGetter = GetRandomNode;

		// create elevator
		elevator = Instantiate (elevatorPrefab.gameObject, this.transform).GetComponent<Elevator>();
		elevator.numNodesInElevator = elevatorWidth;
		elevator.transform.position = Vector2.zero;
		elevator.minHeight = 0;
		elevator.maxHeight = floorHeight * (floors - 1);

		Vector2 size = new Vector2 (elevatorWidth + 2 * floorLength, floorHeight * floors);
		this.GetComponent<BoxCollider2D> ().size = size;
		this.GetComponent<BoxCollider2D> ().offset = new Vector2 (0, size.y / 2 - floorHeight / 2);
	}

	public void Reset() {
		foreach (Floor f in floorArray) {
			if (f.doorLeft.IsOpen()) f.doorLeft.Close ();
			if (f.doorRight.IsOpen()) f.doorRight.Close ();
		}
	}

	Node GetRandomNode() {
		Floor floor = floorArray[rand.Next(1,floorArray.Length)];
		bool left = rand.Next (2) == 0;
		Node[] nodes;
		if (left) {
			nodes = floor.GetOutNodesLeft ();
		} else {
			nodes = floor.GetOutNodesRight ();
		}
		return nodes [nodes.Length - 1];
	}
}

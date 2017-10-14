using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public int floors = 0;
	public int floorLength = 5;
	public int nodeSpacing = 1;
	public int elevatorWidth = 6;
	public float floorHeight = 1;
	public GameObject floorPrefab;
	public GameObject elevatorPrefab;

	// Use this for initialization
	void Start () {
		// create floors
		for (int i = 0; i < floors; ++i) {
			GameObject obj = Instantiate (floorPrefab, this.transform);
			obj.name = "Floor " + i;
			obj.transform.localPosition = new Vector2 (0, floorHeight * i);
			Floor floor = obj.GetComponent<Floor> ();
			floor.halfLength = floorLength;
			floor.middleWidth = elevatorWidth;
			floor.nodeSpacing = nodeSpacing;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

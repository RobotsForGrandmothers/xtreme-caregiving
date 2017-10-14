using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public int floors;
	public int floorLength;
	public float floorHeight;
	public GameObject floorPrefab;
	public GameObject elevatorPrefab;

	// Use this for initialization
	void Start () {
		// create floors
		for (int i = 0; i < floors; ++i) {
			GameObject obj = Instantiate (floorPrefab, this.transform);
			obj.name = "Floor " + i;
			obj.transform.localPosition = new Vector2 (0, floorHeight * i);
			obj.GetComponent<Floor> ().halfLength = floorLength;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

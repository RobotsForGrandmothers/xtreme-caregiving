using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public System.Func<Node> nodeGetter;
	public float firstSpawnTime = 0f;
	public float timeToSpawn = 10f;
	private float nextSpawnTime;
	public Person[] personPrefabs;

	void Start() {
		nextSpawnTime = firstSpawnTime;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= nextSpawnTime) {
			Spawn ();
			nextSpawnTime += timeToSpawn;
		}
	}

	void Spawn() {
		System.Random rand = new System.Random ();
		Person personPrefab = personPrefabs[rand.Next (personPrefabs.Length)];
		Node node = nodeGetter();

		// Don't do anything if the node is reserved
		if (node.IsReserved ()) return;

		Person person = Instantiate (personPrefab).GetComponent<Person>();
		person.transform.position = node.transform.position;
		person.target = node;
	}
}

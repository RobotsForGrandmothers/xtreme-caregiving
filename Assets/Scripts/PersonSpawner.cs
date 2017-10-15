using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public System.Func<Node> nodeGetter;
	public float firstSpawnTime = 0f;
	public float timeToSpawn = 10f;
	private float nextSpawnTime;
	public Person[] personPrefabs;
	public float personSpeed = 2f;
	public float personHungerRate = 100f / 30;
	System.Random rand = new System.Random ((int)(0xa2d10f76 ^ (int)System.DateTime.Now.TimeOfDay.TotalMilliseconds)); // because it works

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
		Person personPrefab = personPrefabs[rand.Next (personPrefabs.Length)];
		Node node = nodeGetter();

		// Don't do anything if the node is reserved
		if (node.IsReserved ()) return;

		Person person = Instantiate (personPrefab).GetComponent<Person>();
		person.transform.position = node.transform.position;
		person.target = node;
		person.hungerRate = personHungerRate;
		person.speed = personSpeed;
	}
}

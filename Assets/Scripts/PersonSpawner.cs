using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour {
	public System.Func<Node> nodeGetter;

	float firstSpawnTime = 0f;
	public float initTimeToSpawn = 3f;
	public float incTimeToSpawn = -1f / 60;
	public float minTimeToSpawn = 1f;
	private float nextSpawnTime;
	public Person[] personPrefabs;
	public float initPersonSpeed = 2f;
	public float incPersonSpeed = 1f / 60;
	public float initPersonHungerRate = 100f / 60;
	public float incPersonHungerRate = 100f / 60 / 60;
	System.Random rand = new System.Random ((int)(0xa2d10f76 ^ (int)System.DateTime.Now.TimeOfDay.TotalMilliseconds)); // because it works
	
	void Start() {
		nextSpawnTime = firstSpawnTime;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time >= nextSpawnTime) {
			Spawn ();
			nextSpawnTime += Mathf.Max(minTimeToSpawn, initTimeToSpawn + incTimeToSpawn * (Time.time - firstSpawnTime));
		}
	}

	void Reset() {
		firstSpawnTime = Time.time;
	}

	void Spawn() {
		Person personPrefab = personPrefabs[rand.Next (personPrefabs.Length)];
		Node node = nodeGetter();

		// Don't do anything if the node is reserved
		if (node.IsReserved ()) return;

		Person person = Instantiate (personPrefab).GetComponent<Person>();
		person.transform.position = node.transform.position;
		person.target = node;
		person.hungerRate = initPersonHungerRate + incPersonHungerRate * (Time.time - firstSpawnTime);
		person.speed = initPersonSpeed + incPersonSpeed * (Time.time - firstSpawnTime);
	}
}

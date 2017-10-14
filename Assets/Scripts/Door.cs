using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour {
	bool _open = false;
	bool open {
		get { return _open; }
		set {
			_open = value;
			this.GetComponent<SpriteRenderer> ().color = (_open ? Color.green : Color.red);
		}
	}

	HashSet<Person> insideDoor = new HashSet<Person> ();

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Person> () != null) {
			insideDoor.Add (other.GetComponent<Person>());
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Person> () != null) {
			insideDoor.Remove (other.GetComponent<Person>());
		}
	}

	public bool IsOpen(){
		return open;
	}

	public void Close(){
		if (open) {
			open = false;
			List<GameObject> toDestroy = new List<GameObject> ();
			foreach (Person p in insideDoor) {
				toDestroy.Add (p.gameObject);
			}
			insideDoor.Clear ();
			for (int i = 0; i < toDestroy.Count; ++i) {
				Destroy (toDestroy[i]);
			}
		} else {
			throw new Exception ("door is already closed");
		}
	}
	public void Open(){
		if (!open) {
			open = true;
		} else {
			throw new Exception ("door is already open");
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

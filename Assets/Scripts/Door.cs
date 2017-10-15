using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour {
	Animator animator;
	
	bool _open = false;
	bool open {
		get { return _open; }
		set {
			_open = value;
			//this.GetComponent<SpriteRenderer> ().color = (_open ? Color.green : Color.red);
			animator.SetBool ("Open", _open);
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
			//List<GameObject> toDestroy = new List<GameObject> ();
			foreach (Person p in insideDoor) {
				//toDestroy.Add (p.gameObject);
				p.Kill ();
			}
			insideDoor.Clear ();
			//for (int i = 0; i < toDestroy.Count; ++i) {
			//	Destroy (toDestroy[i]);
            //    Despawner.deaths += 1;
            //    Debug.Log("another dead person, total = " + Despawner.deaths);
			//}
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
		animator = GetComponent<Animator> ();
		open = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

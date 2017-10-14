using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Door : MonoBehaviour {
	bool open = false;

	public void Close(){
		if (open) {
			open = false;
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

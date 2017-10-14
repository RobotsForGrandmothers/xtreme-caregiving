using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour {
	bool reserved = false;
	public Person reserver { get; private set; }
	public Node left;
	public Node right;

	public bool IsReserved() {
		return reserved;
	}

	public void Reserve(Person reserver) {
		if (!reserved) {
			reserved = true;
			this.reserver = reserver;
		} else {
			throw new Exception ("The node is already reserved");
		}
	}

	public void Unreserve(Person reserver) {
		if (reserved) {
			if (reserver == this.reserver) {
				reserved = false;
				this.reserver = null;
			} else {
				throw new Exception ("The node was not reserved by " + reserver);
			}
		} else {
			throw new Exception ("The node has not been reserved");
		}
	}
}

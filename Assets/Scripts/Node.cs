using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour {
	bool reserved = false;
	public Node left;
	public Node right;

	public bool IsReserved() {
		return reserved;
	}

	public void Reserve() {
		if (!reserved) {
			reserved = true;
		} else {
			throw new Exception ("The node is already reserved");
		}
	}

	public void Unreserve() {
		if (reserved) {
			reserved = false;
		} else {
			throw new Exception ("The node has not been reserved");
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Floor : MonoBehaviour {
	public GameObject doorPrefab;

	int _halfLength;
	public int halfLength { 
		get { return _halfLength; } 
		set { _halfLength = value; RecreateNodes (); } 
	}
	float _nodeSpacing = 1;
	public float nodeSpacing {
		get { return _nodeSpacing; }
		set { _nodeSpacing = value; RepositionNodes (); }
	}
	float _middleWidth = 1;
	public float middleWidth {
		get { return _middleWidth; }
		set { _middleWidth = value; RepositionNodes (); }
	}
	Node[] leftNodes;
	Node[] rightNodes;
	public Node leftNodeShaft { get; private set; }
	public Node rightNodeShaft { get; private set; }
	public Door left;
	public Door right;

	// Use this for initialization
	void Start () {
		left = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		left.transform.localPosition = new Vector2(-middleWidth / 2, 0);
		right = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		right.transform.localPosition = new Vector2(+middleWidth / 2, 0);
		Vector3 rightScale = right.transform.localScale;
		rightScale.x *= -1;
		right.transform.localScale = rightScale;

		RecreateNodes ();

		// set up collider for elevator
		this.GetComponent<BoxCollider2D> ().size = new Vector2 (middleWidth, 1);
	}

	void RecreateNodes() {
		// delete old nodes
		if (leftNodes != null) {
			foreach (Node node in leftNodes) {
				Destroy (node.gameObject);
			}
		}
		if (rightNodes != null) {
			foreach (Node node in rightNodes) {
				Destroy (node.gameObject);
			}
		}
		if (leftNodeShaft != null) {
			Destroy (leftNodeShaft.gameObject);
		}
		if (rightNodeShaft != null) {
			Destroy (rightNodeShaft.gameObject);
		}

		leftNodes = new Node[halfLength * 2];
		rightNodes = new Node[halfLength * 2];

		// create shaft nodes
		leftNodeShaft = CreateNode ("Node Shaft Left");
		leftNodeShaft.isDeath = true;
		rightNodeShaft = CreateNode ("Node Shaft Right");
		rightNodeShaft.isDeath = true;

		// create nodes going into each side of the floor
		for (int i = 0; i < halfLength; ++i) {
			leftNodes [i] = CreateNode("Node Left In " + i);
			rightNodes [i] = CreateNode ("Node Right In " + i);

			if (i > 0) {
				leftNodes [i - 1].left = leftNodes [i];
				rightNodes [i - 1].right = rightNodes [i];
			}
		}

		// create the nodes going back out of the floor, starting from farthest in the room
		for (int i = 0; i < halfLength; ++i) {
			int index = 2 * halfLength - i - 1;
			leftNodes [index] = CreateNode("Node Left Out " + i);
			rightNodes [index] = CreateNode ("Node Right Out " + i);

			if (i > 0) {
				leftNodes [index].right = leftNodes [index + 1];
				rightNodes [index].left = rightNodes [index + 1];
			}
		}
		
		// link up going in and going out
		if (halfLength > 0) {
			leftNodes [halfLength - 1].right = leftNodes [halfLength];
			rightNodes [halfLength - 1].left = rightNodes [halfLength];
		}

		// reposition new nodes
		RepositionNodes ();
	}

	void RepositionNodes() {
		leftNodeShaft.transform.localPosition = new Vector2 (-(middleWidth / 2 * nodeSpacing), 0);
		rightNodeShaft.transform.localPosition = new Vector2 (+(middleWidth / 2 * nodeSpacing), 0);
		for (int i = 0; i < halfLength; ++i) {
			leftNodes [i].transform.localPosition = new Vector2 (-(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
			rightNodes [i].transform.localPosition = new Vector2 (+(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
		}
		for (int i = 0; i < halfLength; ++i) {
			int index = 2 * halfLength - i - 1;
			leftNodes [index].transform.localPosition = new Vector2 (-(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
			rightNodes [index].transform.localPosition = new Vector2 (+(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
		}
	}

	public Node GetEntranceLeft() {
		return leftNodes [0];
	}
	public Node GetEntranceRight() {
		return rightNodes [0];
	}
	public Node GetExitLeft() {
		return leftNodes [leftNodes.Length - 1];
	}
	public Node GetExitRight() {
		return rightNodes [rightNodes.Length - 1];
	}
	public Node[] GetOutNodesLeft() {
		Node[] outNodes = new Node[halfLength];
		for (int i = 0; i < halfLength; ++i) {
			int index = 2 * halfLength - i - 1;
			outNodes [i] = leftNodes [index];
		}
		return outNodes;
	}
	public Node[] GetOutNodesRight() {
		Node[] outNodes = new Node[halfLength];
		for (int i = 0; i < halfLength; ++i) {
			int index = 2 * halfLength - i - 1;
			outNodes [i] = rightNodes [index];
		}
		return outNodes;
	}

	Node CreateNode(string name) {
		GameObject node = new GameObject(name);
		node.transform.SetParent (this.transform);
		node.AddComponent<Node>();
		return node.GetComponent<Node> ();
	}
}

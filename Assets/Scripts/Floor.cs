using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public GameObject doorPrefab;

	int _halfLength;
	public int halfLength { get { return _halfLength; } set { _halfLength = value; RecreateNodes (); } }
	public float nodeSpacing = 1;
	public float middleWidth = 1;
	Node[] leftNodes;
	Node[] rightNodes;
	public Door left;
	public Door right;

	// Use this for initialization
	void Start () {
		left = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		left.transform.localPosition = new Vector2(-middleWidth / 2, 0);
		right = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		right.transform.localPosition = new Vector2(+middleWidth / 2, 0);

		RecreateNodes ();
	}

	void RecreateNodes() {
		// delete old nodes
		if (leftNodes != null) {
			foreach (Node node in leftNodes) {
				Destroy (node);
			}
		}
		if (rightNodes != null) {
			foreach (Node node in rightNodes) {
				Destroy (node);
			}
		}

		leftNodes = new Node[halfLength * 2];
		rightNodes = new Node[halfLength * 2];

		// create nodes going into each side of the floor
		for (int i = 0; i < halfLength; ++i) {
			leftNodes [i] = CreateNode("Node Left In " + i);
			rightNodes [i] = CreateNode ("Node Right In " + i);
			leftNodes [i].transform.transform.localPosition = new Vector2 (-(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
			rightNodes [i].transform.transform.localPosition = new Vector2 (+(middleWidth / 2 + (i + 1) * nodeSpacing), 0);

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
			leftNodes [index].transform.transform.localPosition = new Vector2 (-(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
			rightNodes [index].transform.transform.localPosition = new Vector2 (+(middleWidth / 2 + (i + 1) * nodeSpacing), 0);

			if (i > 0) {
				leftNodes [index].right = leftNodes [index + 1];
				rightNodes [index].left = rightNodes [index + 1];
			}
		}

		if (halfLength > 0) {
			// link up going in and going out
			leftNodes [halfLength - 1].right = leftNodes [halfLength];
			rightNodes [halfLength - 1].left = rightNodes [halfLength];
		}
	}

	public Node GetEntranceLeft() {
		return leftNodes [0];
	}
	public Node GetEntranceRight() {
		return rightNodes [0];
	}
	public Node GetExitLeft() {
		return leftNodes [leftNodes.Length];
	}
	public Node GetExitRight() {
		return rightNodes [rightNodes.Length];
	}


	Node CreateNode(string name) {
		GameObject node = new GameObject(name);
		node.transform.SetParent (this.transform);
		node.AddComponent<Node>();
		return node.GetComponent<Node> ();
	}
}

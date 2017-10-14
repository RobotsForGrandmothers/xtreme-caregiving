using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public int halfLength;
	public float nodeSpacing = 1;
	public float middleWidth = 1;
	Node[] nodes;

	// Use this for initialization
	void Start () {
		nodes = new Node[halfLength * 2];

		for (int i = 0; i < halfLength; ++i) {
			nodes [i] = CreateNode(i);
			nodes [i + halfLength] = CreateNode (i + halfLength);
			nodes [i].transform.transform.localPosition = new Vector2 (-(middleWidth / 2 + i * nodeSpacing), 0);
			nodes [i + halfLength].transform.transform.localPosition = new Vector2 (+(middleWidth / 2 + i * nodeSpacing), 0);

			if (i > 0) {
				nodes [i].right = nodes [i - 1];
				nodes [i - 1].left = nodes [i];
				nodes [i + halfLength].left = nodes [i + halfLength - 1];
				nodes [i + halfLength - 1].right = nodes [i + halfLength];
			}
		}
	}

	Node CreateNode(int index) {
		GameObject node = new GameObject("Node " + index);
		node.transform.SetParent (this.transform);
		node.AddComponent<Node>();
		return node.GetComponent<Node> ();
	}
}

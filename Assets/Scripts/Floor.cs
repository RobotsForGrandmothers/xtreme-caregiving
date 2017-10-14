using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public GameObject doorPrefab;

	public int halfLength;
	public float nodeSpacing = 1;
	public float middleWidth = 1;
	Node[] nodes;
	Door left;
	Door right;

	// Use this for initialization
	void Start () {
		left = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		left.transform.localPosition = new Vector2(-middleWidth / 2, 0);
		right = Instantiate (doorPrefab, this.transform).GetComponent<Door>();
		right.transform.localPosition = new Vector2(+middleWidth / 2, 0);

		nodes = new Node[halfLength * 2];

		for (int i = 0; i < halfLength; ++i) {
			nodes [i] = CreateNode(i);
			nodes [i + halfLength] = CreateNode (i + halfLength);
			nodes [i].transform.transform.localPosition = new Vector2 (-(middleWidth / 2 + (i + 1) * nodeSpacing), 0);
			nodes [i + halfLength].transform.transform.localPosition = new Vector2 (+(middleWidth / 2 + (i + 1) * nodeSpacing), 0);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
	public int length;
	public float nodeSpacing = 1;
	Node[] nodes;

	// Use this for initialization
	void Start () {
		nodes = new Node[length];

		for (int i = 0; i < length; ++i) {
			GameObject node = new GameObject("Node " + i);
			node.transform.SetParent (this.transform);
			node.AddComponent<Node>();
			nodes [i] = node.GetComponent<Node>();
			nodes [i].transform.transform.localPosition = new Vector2 (i * nodeSpacing, 0);

			if (i > 0) {
				nodes [i].left = nodes [i - 1];
				nodes [i - 1].right = nodes [i];
			}
		}
	}
}

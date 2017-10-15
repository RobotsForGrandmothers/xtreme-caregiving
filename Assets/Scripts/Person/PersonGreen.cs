using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonGreen : Person {
	static System.Random rand = new System.Random ();
	public List<Sprite> sprites;
	public SpriteRenderer spriteRenderer;

	// Use this for initialization
	new void Start () {
		base.Start ();
		spriteRenderer.transform.parent = this.transform;
		spriteRenderer.sprite = sprites [rand.Next (sprites.Count)];

	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}
}

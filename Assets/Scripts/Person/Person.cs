using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : MonoBehaviour {
	private static float reachedDistance = 0.02f;

    public float speed = 1f;
    private bool isFacingRight;
	Node _target;
	public Node target {
		get { return _target; }
		set { 
			if (value != null) value.Reserve ();
			if (_target != null) _target.Unreserve ();
			_target = value;
			moving = true;
		}
	}
	private bool moving;

	// Use this for initialization
	protected void Start () {
		
	}
	
	// Update is called once per frame
	protected void Update () {
		// if we have no target, return
		if (target == null) return;

		// if we're moving, move towards our target
		if (moving) {
			Vector3 position = this.transform.position;
			float deltaTarget = target.transform.position.x - position.x;
			float deltaX = speed * Time.deltaTime * Mathf.Sign(deltaTarget);

			// If we can reach our target, do it
			if (Mathf.Abs (deltaX) > Mathf.Abs (deltaTarget)) {
				deltaX = deltaTarget;
				moving = false;
			}
			position.x = position.x + deltaX;
		}

		// if we're not moving get a new target
		if (!moving) {
			if (isFacingRight) {
				if (target.right == null) {// turn if we must
					isFacingRight = false;
				} else if (!target.right.IsReserved ()) { // keep moving if we can
					target = target.right;
				}
			} else {
				if (target.left == null) {// turn if we must
					isFacingRight = true;
				} else if (!target.left.IsReserved ()) { // keep moving if we can
					target = target.right;
				}
			}
		}
	}
}

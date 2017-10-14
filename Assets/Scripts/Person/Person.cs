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
			if (value != null) value.Reserve (this);
			if (_target != null) _target.Unreserve (this);
			_target = value;

			if (_target != null) {
				this.transform.parent = _target.transform;
				this.transform.localPosition = new Vector2 (this.transform.localPosition.x, 0);
			}
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
			this.transform.position = position;
		}

		// if we're not moving get a new target
		if (!moving) {
			if (isFacingRight) {
				if (target.right == null) {// turn if we must
					isFacingRight = false;
				} else if (!target.right.IsReserved ()) { // keep moving if we can
					target = target.right;
				} else { // try to swap if we can
					if (target.right.left == target && !target.right.reserver.isFacingRight) {
						Node otherTarget = target;
						target = null;
						otherTarget.right.reserver.target = otherTarget;
						target = otherTarget.right;
					}
				}
			} else {
				if (target.left == null) {// turn if we must
					isFacingRight = true;
				} else if (!target.left.IsReserved ()) { // keep moving if we can
					target = target.left;
				} else { // try to swap if we can
					if (target.left.right == target && target.left.reserver.isFacingRight) {
						Node otherTarget = target;
						target = null;
						otherTarget.left.reserver.target = otherTarget;
						target = otherTarget.left;
					}
				}
			}
		}
	}
}

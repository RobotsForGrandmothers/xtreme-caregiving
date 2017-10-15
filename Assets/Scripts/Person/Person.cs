using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Person : MonoBehaviour {
	private static float reachedDistance = 0.02f;

	static System.Random rand = new System.Random();

	public bool crazy = false;

    public float speed = 2f;
	public float hungerRate = 100f / 30;
	float _hunger = 0;
	public float hunger {
		get { return _hunger; }
		set { 
			_hunger = value;
			this.GetComponent<SpriteRenderer> ().color = new Color (1, 1 - (hunger / 100), 1 - (hunger / 100));
			if (_hunger >= 100) this.Kill ();
		}
	}
    
	bool _isFacingRight;
	private bool isFacingRight {
 		get { return _isFacingRight; }
		set { _isFacingRight = value; 
			Vector3 scale = this.gameObject.transform.localScale;
			scale.x = _isFacingRight ? 1 : -1;
			this.gameObject.transform.localScale = scale;
		}
	}

	public bool dead { get; private set; }

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

    // on destroy
    private void OnDestroy()
    {
        target = null;
    }

    // Use this for initialization
    protected void Start () {
		isFacingRight = true;	
	}

	public void Kill() {
		this.dead = true;
		this.target = null;
		this.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Dynamic;
		this.GetComponent<Rigidbody2D> ().velocity = speed * (isFacingRight ? Vector2.right : Vector2.left);
		this.GetComponent<Rigidbody2D> ().angularVelocity = isFacingRight ? -180 : 180;

		ScoreTracking.globalData.deaths += 1;
	}

	// Destroy when leaving the building collider
	void OnTriggerExit2D(Collider2D other) {
		if (other.GetComponent<Building> () != null) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	protected void Update () {
		// if we're dead, return
		if (dead) return;

		// increment hunger
		hunger += hungerRate * Time.deltaTime;

		// if we have no target, return
		if (target == null) return;

		// if we're crazy, turn with time based probability
		if (rand.NextDouble () <= 2 * (1f / (1 + Mathf.Exp (-Time.deltaTime)) - 0.5f)) {
			isFacingRight = !isFacingRight;
		}

		// if we're moving, move towards our target
		if (moving) {
			Vector3 position = this.transform.position;
			float deltaTarget = target.transform.position.x - position.x;
			float deltaX = speed * Time.deltaTime * Mathf.Sign(deltaTarget);

			// If we can reach our target, do it
			if (Mathf.Abs (deltaX) > Mathf.Abs (deltaTarget)) {
				deltaX = deltaTarget;
				moving = false;
				if (target.isDeath) this.Kill ();
			}
			position.x = position.x + deltaX;
			this.transform.position = position;
		}

		// if we're not moving get a new target
		if (!moving) {
			if (isFacingRight) {
				if (target.right == null) {// turn if we must, but not in an elevator
					if (!target.inElevator && target.left != null) {
						isFacingRight = false;
					}
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
					if (!target.inElevator && target.right != null) {
						isFacingRight = true;
					}
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

using UnityEngine;
using System.Collections;

public class Bird_Controller : MonoBehaviour {

	public Transform target;
	public Transform destination;
	public float airSpeed;

	private bool isFree = false;
	private bool getTheTarget = false;
	private bool getTheDestination = false;
	private bool facingRight = false;
	private Transform currentTarget;
	private Vector2 spriteSize;
	private Vector2 localSpriteSize;
	private Vector3 newPosition;

	void Start () {
		if (target != null) {
			Transform child = target.GetChild(0);
			spriteSize = child.GetComponent<SpriteRenderer> ().sprite.rect.size;
			localSpriteSize = spriteSize / child.GetComponent<SpriteRenderer> ().sprite.pixelsPerUnit;
			currentTarget = target;
		} else {
			Debug.Log ("target object is missing");
		}
	}
	
	void Update () {
		if (isFree) {

			if (transform.position.x - currentTarget.position.x < 0 && !facingRight ||
				transform.position.x - currentTarget.position.x > 0 && facingRight) {
				Flip ();
			}

			if (!getTheTarget) {
				transform.position = Vector3.MoveTowards (transform.position, currentTarget.position, airSpeed * Time.deltaTime);
				if (transform.position == currentTarget.position) {
					currentTarget = destination;
					newPosition = new Vector3 (destination.position.x, destination.position.y, destination.position.z);
					currentTarget.position = newPosition;
					getTheTarget = true;
				}
			} else if (getTheTarget && !getTheDestination) {
				transform.position = Vector3.MoveTowards (transform.position, currentTarget.position, airSpeed * Time.deltaTime);
				target.position = Vector3.MoveTowards (target.position, currentTarget.position, airSpeed * Time.deltaTime);
				if (transform.position == currentTarget.position) {
					currentTarget.position = new Vector3 (Camera.main.orthographicSize, Camera.main.orthographicSize * 2, destination.position.z);
					getTheDestination = true;
				}
			} else if (getTheTarget && getTheDestination) {
				//Fly to infinity
				transform.position = Vector3.MoveTowards (transform.position, currentTarget.position, airSpeed * Time.deltaTime * 3);
				Destroy (transform.gameObject, 2f);
			}
		}
	}
	
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void SetFree() {
		isFree = true;
	}
}

using UnityEngine;
using System.Collections;

public class routing : MonoBehaviour {

	public float speed = 5;
	public Transform[] destinationPositions;

	private bool facingRight = true;
	private int transformIndex = 0;
	void Start () {
		
	}
	
	void Update() {
		if (transformIndex < destinationPositions.Length) {
			transform.position = Vector3.MoveTowards (transform.position, destinationPositions [transformIndex].position, speed * Time.deltaTime);
			turnItemToDirection (transformIndex);
			if (transform.position == destinationPositions [transformIndex].position) {
				transformIndex++;
			}
		} else if (transformIndex == destinationPositions.Length) {
			gameObject.SetActive (false);
		}
	}
		
	void turnItemToDirection(int index) {
		Vector3 relativePos = destinationPositions [index].transform.position - transform.position;
		float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		if (relativePos.x > 0 && !facingRight || relativePos.x < 0 && facingRight) {
			Flip ();
		}



	}
	void Flip() {
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.y *= -1;
		transform.localScale = scale;
	}
}

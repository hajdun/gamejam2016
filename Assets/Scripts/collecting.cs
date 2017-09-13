using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class collecting : MonoBehaviour {

	private Rigidbody2D rigidbody2D;
	private bool canCollect;
	private Collider2D other;
	private DisplayController displayController;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		GameObject displayObject = GameObject.FindGameObjectWithTag ("DisplayController");
		displayController = displayObject.gameObject.transform.GetComponent<DisplayController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (canCollect && Input.GetButtonDown("Collect")) {

			if (displayController.AddItem (other.tag)) {
				canCollect = false;
				other.gameObject.SetActive (false);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		canCollect = true;
		this.other = other;
	}

	void OnTriggerExit2D() {
		canCollect = false;
	}
}

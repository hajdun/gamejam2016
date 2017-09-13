using UnityEngine;
using System.Collections;

public class cage_controller : MonoBehaviour {

	public GameObject textBuble;
	public Bird_Controller birdController;
	public GameObject destroyAnim;
	public int questId = 2;

	private GameObject player;
	private DisplayController displayController;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		GameObject displayObject = GameObject.FindGameObjectWithTag ("DisplayController");
		displayController = displayObject.gameObject.transform.GetComponent<DisplayController>();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (displayController.HasAllItems(questId)) {
				//Do animation, free the bird, deactive the object
				Instantiate(destroyAnim, transform.position, Quaternion.identity);
				birdController.SetFree();
				gameObject.SetActive (false);
				displayController.QuestCompleted (questId);
			} else {
				textBuble.gameObject.SetActive (true);
				displayController.QuestStarted (questId);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			textBuble.gameObject.SetActive (false);
		}
	}
}

using UnityEngine;
using System.Collections;

public class squirrel_controller : MonoBehaviour {

	public GameObject textBuble;
	public GameObject heartBuble;
	public GameObject runningSquirrel;
	public float sayThankYou = 2f;
	public int countOfCollectableItems = 1;
	public int questNumber = 1;
	public bool testQuestComplete = false;

	private GameObject player;
	//private collecting playerCollecting;
	//private bool isQuestCompleted = false;
	private DisplayController displayController;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		/*playerCollecting = player.gameObject.transform.GetComponent<collecting> ();
		if (playerCollecting == null) {
			Debug.Log ("collecting script is missing");
		}*/
		runningSquirrel.gameObject.SetActive (false);
		GameObject displayObject = GameObject.FindGameObjectWithTag ("DisplayController");
		displayController = displayObject.gameObject.transform.GetComponent<DisplayController>();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && !displayController.IsQuestCompleted(questNumber)) {
			if (displayController.HasAllItems(questNumber) || testQuestComplete) {
				//Quest completed
				displayController.QuestCompleted(questNumber);
				StartCoroutine (WaitForThanks ());
			} else {
				textBuble.gameObject.SetActive (true);
				displayController.QuestStarted (questNumber);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			textBuble.gameObject.SetActive (false);
		}
	}
		
	private IEnumerator WaitForThanks() {
		heartBuble.gameObject.SetActive (true);
		yield return new WaitForSeconds (sayThankYou);
		heartBuble.SetActive (false);
		gameObject.SetActive (false);
		runningSquirrel.gameObject.SetActive (true);
	}
}

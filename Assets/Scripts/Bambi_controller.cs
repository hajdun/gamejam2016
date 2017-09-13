using UnityEngine;
using System.Collections;

public class Bambi_controller : MonoBehaviour {

	public GameObject textBuble;
	public GameObject heartBuble;
	public GameObject runningSquirrel;
	public float sayThankYou = 2f;
//	public int countOfCollectableItems = 1;
	public int questId = 0;

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
		GameObject displayObject = GameObject.FindGameObjectWithTag ("DisplayController");
		displayController = displayObject.gameObject.transform.GetComponent<DisplayController>();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && !displayController.IsQuestCompleted(questId)) {
			if (displayController.HasAllItems(questId)) {
				//Quest completed
				displayController.QuestCompleted(questId);
				StartCoroutine (WaitForThanks ());
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

    private IEnumerator WaitForThanks() {
        heartBuble.gameObject.SetActive(true);
        yield return new WaitForSeconds(sayThankYou);
        heartBuble.SetActive(false);
        //isQuestCompleted = true;
        GameObject.Find("Scene_Controller").GetComponent<Scene_controller>().loadNextLevel();
    }
}

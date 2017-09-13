using UnityEngine;
using System.Collections;

public class CaveScript : MonoBehaviour {

	public int[] questsNeedTobeDone;

	private DisplayController displayController;

	void Start () {
		GameObject displayObject = GameObject.FindGameObjectWithTag ("DisplayController");
		displayController = displayObject.gameObject.transform.GetComponent<DisplayController>();
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			bool allQuestDone = true;
			for (int i = 0; i < questsNeedTobeDone.Length; i++) {
				if (!displayController.IsQuestCompleted (questsNeedTobeDone [i])) {
					allQuestDone = false;
					break;
				}
			}

			if (allQuestDone) {
				GameObject.Find ("Scene_Controller").GetComponent<Scene_controller> ().loadNextLevel ();
			}
		}
	}
}

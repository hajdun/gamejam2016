using UnityEngine;
using System.Collections;

public class DisplayController : MonoBehaviour {

	public Quest[] quests;


	void Start () {
		for (int i = 0; i < quests.Length; i++) {
			quests [i].Init ();
		}
	}

	public void QuestStarted(int questId) {
		for (int i = 0; i < quests.Length; i++) {
			if (quests [i].questId == questId) {
				quests [i].SetQuestActive (true);
			}
		}
	}

	public void QuestCompleted(int questId) {
		for (int i = 0; i < quests.Length; i++) {
			if (quests [i].questId == questId) {
				quests [i].QuestIsDone();
			}
		}
	}

	public bool IsQuestCompleted(int questId) {
		for (int i = 0; i < quests.Length; i++) {
			if (quests [i].questId == questId) {
				return quests [i].IsQuestFinished ();
			}
		}

		return false;
	}

	public bool AddItem(string tagName) {
		bool canCollect = false;
		for (int i = 0; i < quests.Length; i++) {
			if (tagName == quests [i].itemName) {
				canCollect = true;
				quests [i].IncreaseNumberOfItem ();
			}
		}

		return canCollect;
	}

	public bool HasAllItems(int questId) {

		for (int i = 0; i < quests.Length; i++) {
			if (quests [i].questId == questId) {
				if (quests [i].GetMaxItemCount () == quests [i].GetCurrentItemCount ()) {
					return true;
				}
			}
		}

		return false;
	}


}

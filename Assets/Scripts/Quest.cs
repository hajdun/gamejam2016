using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[Serializable]
public class Quest {

	public GameObject collectableItem;
	public Text resultText;
	public int questId;
	[HideInInspector] public string itemName;

	private bool questActive = false;
	private bool questDone = false;
	private GameObject item;
	private int maxCount;
	private int currentCount;

	public void Init() {
		itemName = collectableItem.tag;
		maxCount = GameObject.FindGameObjectsWithTag (itemName).Length - 1;
		currentCount = 0;
		collectableItem.SetActive (false);
		resultText.text = string.Empty;
	}

	public bool GetQuestActive() {
		return questActive;
	}

	public void SetQuestActive(bool status) {
		if (status && !questActive) {
			questActive = true;
			collectableItem.SetActive (true);
			RefreshText ();
		} else if (!status && questActive) {
			questActive = false;
			collectableItem.SetActive (false);
			resultText.text = string.Empty;
		}
	}

	public void QuestIsDone() {
		if (!questDone) {
			questDone = true;
			SetQuestActive (false);
		}
	}

	public void IncreaseNumberOfItem() {
		currentCount++;
		RefreshText ();
	}

	private void RefreshText() {
		if (questActive) {
			resultText.text = currentCount + " / " + maxCount;
		}
	}
		
	public int GetMaxItemCount() {
		return maxCount;
	}

	public int GetCurrentItemCount() {
		return currentCount;
	}

	public bool IsQuestFinished() {
		return questDone;
	}
}
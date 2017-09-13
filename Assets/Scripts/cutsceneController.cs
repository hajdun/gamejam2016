using UnityEngine;
using System.Collections;

public class cutsceneController : MonoBehaviour {

	public bool animationFinished = false;
    private float skip_timer = 3.0f;
    public GameObject skip_button;


    // Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            allow_skip();
        }
		if (animationFinished) skip();
    }

    private void allow_skip() {
        skip_button.SetActive(true);
    }

    public void skip() {
        GameObject.Find("Scene_Controller").GetComponent<Scene_controller>().loadNextLevel();
    }
}

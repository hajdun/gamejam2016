using UnityEngine;
using System.Collections;

public class roll_credits : MonoBehaviour {

    private float scroll_speed;
    private float credits_init_pos;
    public GameObject myMenu_hub;
    public menu_controller myMenu_controller;

	// Use this for initialization
	void Start () {
        scroll_speed = 1.2f;
        credits_init_pos = this.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (myMenu_controller.enabled) {
            myMenu_controller.enabled = false;
        }
        if (this.transform.position.y - credits_init_pos > 30 || Input.anyKeyDown) {
            this.transform.position = new Vector3(this.transform.position.x, credits_init_pos, this.transform.position.z);
            myMenu_controller.enabled = true;
            myMenu_controller.choose_menu(myMenu_hub);
        }
        else {
            this.transform.Translate(Vector3.up * Time.unscaledDeltaTime * scroll_speed);
        }
    }
}

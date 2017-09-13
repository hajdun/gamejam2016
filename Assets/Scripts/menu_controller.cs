using UnityEngine;
using System.Collections;

public class menu_controller : MonoBehaviour {

    private bool inMenu;
    public GameObject myMenu_hub;
    public GameObject myMenu_credits;
    private GameObject deer;
    private GameObject scene_controller;
    private AudioSource[] FX;
    private GameObject myMenu_current;
    private DeerController myDeer_controller;


    // Use this for initialization
    void Start () {
        inMenu = false;
        myMenu_current = myMenu_hub;
        deer = GameObject.Find("deer");
        scene_controller = GameObject.Find("Scene_Controller");
        FX = AudioSource.FindObjectsOfType<AudioSource>();
        if (deer != null) myDeer_controller = deer.GetComponent<DeerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (deer != null) {
            if (!inMenu && Input.GetKeyDown(KeyCode.Escape)) {
                myDeer_controller.enabled = false;
                inMenu = true;
                myMenu_hub.SetActive(true);
                Time.timeScale = 0;
                for (int i = 0; i < FX.Length; i++) {
                    if (FX[i].name != "background_music") {
                        FX[i].Stop();
                    }
                }
            }
            else if (inMenu && Input.GetKeyDown(KeyCode.Escape)) {
                Resume();
            }
        }
	}

    public void choose_menu(GameObject menu) {
        myMenu_current.SetActive(false);
        myMenu_current = menu;
        menu.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1;
        myMenu_current.SetActive(false);
        myMenu_current = myMenu_hub;
        myDeer_controller.enabled = true;
        inMenu = false;
    }
    
    public void run_credits() {
        choose_menu(myMenu_credits);
    }

    public void Quit() {
        Resume();
        scene_controller.GetComponent<Scene_controller>().loadMainMenu();
    }

    public void Exit(){
        Application.Quit();
    }

}

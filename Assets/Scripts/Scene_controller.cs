using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Scene_controller : MonoBehaviour {

    private int level_num;
    private int level_max;
    private Slider fx;
    private float fxVolumeLevel;
    private Slider music;
    private float musicVolumeLevel;
    private float defaultVolumeLevel;
    private GameObject menu_canvas;

    private float fadeSpeed = 0.8f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    public Texture2D fadeOutTexture;

    void OnGUI() {

        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),fadeOutTexture);
    }

    private void BeginFade (int direction) {
        fadeDir = direction;
    }

    void Awake() {
        DontDestroyOnLoad(this);
    }

    void OnLevelWasLoaded() {
        BeginFade(-1);
        waitUp(fadeSpeed);
        findSlider();
    }

    // Use this for initialization
    void Start () {
        defaultVolumeLevel = 1.0f;
        fxVolumeLevel = defaultVolumeLevel;
        musicVolumeLevel = defaultVolumeLevel;
        level_num = SceneManager.GetActiveScene().buildIndex;
        level_max = SceneManager.sceneCountInBuildSettings;
        findSlider();
    }

    public void loadNextLevel() {
        this.level_num++;
        loadLevel();
    }

    public void loadMainMenu() {
        this.level_num = 0;
        loadLevel();
        DestroyImmediate(gameObject);
    }

    private void loadLevel() {
        if (fx != null) VolumeCheck_FX();

        if (music != null) VolumeCheck_Music();

        BeginFade(1);
        waitUp(fadeSpeed);

        if (level_num < level_max) SceneManager.LoadScene(level_num);
        else loadMainMenu();
    } 

    private IEnumerator waitUp (float time) {
        yield return new WaitForSeconds(time);
    }

    public void VolumeCheck_FX() {
        fxVolumeLevel = fx.normalizedValue;
    }

    public void VolumeCheck_Music() {
        musicVolumeLevel=music.normalizedValue;
    }

    private void findSlider() {
        Slider[] volume = new Slider[2];
        menu_canvas = GameObject.Find("Menu");
        if (menu_canvas != null) {
            volume = menu_canvas.GetComponentsInChildren<Slider>(true);
            for (int i = 0; i <= 1; i++) {
                if (volume[i] != null) {
                    switch (i) {
                        case 0:
                            fx = volume[i];
                            fx.normalizedValue = fxVolumeLevel;
                            break;
                        case 1:
                            music = volume[i];
                            music.normalizedValue = musicVolumeLevel;
                            break;
                    }
                }
                else {
                    Debug.LogError(i + " not found");
                }
            }
        }
    }
}

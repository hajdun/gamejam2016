using UnityEngine;
using System.Collections;

public class deer_following_camera : MonoBehaviour {
	public float dampTime = 0.15f;
	public GameObject target;
	public Vector2 offset = new Vector2 (0.5f, 0.3f);
	public BoxCollider2D Bounds;
    private float keyDownTimer;
    private float keyDownTime;

	Vector3 velocity = Vector3.zero;
	Transform targetTransform;
	DeerController deerController;

	private Vector3 boundsMin, boundsMax;

	void Start() {
		boundsMin = Bounds.bounds.min;
		boundsMax = Bounds.bounds.max;
		targetTransform = target.GetComponent<Transform> ();
		deerController = target.GetComponent<DeerController> ();
        keyDownTime = 0.5f;
        keyDownTimer = keyDownTime;
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (keyDownTimer > 0) keyDownTimer -= Time.deltaTime;
            if (keyDownTimer <= 0) transform.position = Vector3.SmoothDamp(transform.position, calculateDestination() - Vector3.up, ref velocity, dampTime);
        }
        else if (targetTransform) {
                //Smoothly damp the camera's position based on the calculated values
                transform.position = Vector3.SmoothDamp(transform.position, calculateDestination(), ref velocity, dampTime);
            }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) {
            keyDownTimer = keyDownTime;
        }
	}

	Vector3 calculateDestination() {
		var cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
		//Get the position of the attached transform in Unity world position
		Vector3 point = Camera.main.WorldToViewportPoint(targetTransform.position);
		//Create a position with the delta
		Vector3 delta = targetTransform.position - Camera.main.ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
		//Add the delta to the target position
		Vector3 destination = transform.position + delta;
		return new Vector3(
			Mathf.Clamp (destination.x, boundsMin.x + cameraHalfWidth, boundsMax.x - cameraHalfWidth),
			Mathf.Clamp (destination.y, boundsMin.y + Camera.main.orthographicSize, boundsMax.y - Camera.main.orthographicSize),
			destination.z);
	}
}

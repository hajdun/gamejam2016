using UnityEngine;
using System.Collections;

public class RepeatSpriteBoundary : MonoBehaviour {

	SpriteRenderer groundTop;
	float slopeAngle;
	public SpriteRenderer groundBottom;


	void Awake() {
		//initialization
		groundTop = GetComponent<SpriteRenderer>();
		//save and reset rotation to 0 of parent element
		slopeAngle = groundTop.transform.rotation.eulerAngles.z;
		groundTop.transform.localRotation = Quaternion.identity;

		Vector2 originalGroundTopSize = new Vector2(groundTop.bounds.size.x / transform.localScale.x, groundTop.bounds.size.y / transform.localScale.y);

		// Generate a child prefab of the sprite renderer
		GameObject childPrefab = new GameObject();
		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
		childPrefab.transform.position = transform.position;
		childPrefab.GetComponent<SpriteRenderer> ().sortingOrder = groundTop.sortingOrder;
		childSprite.sprite = groundTop.sprite;

		// Loop through and spit out repeated tiles
		GameObject child;
		Vector2 horizontalBoundary = new Vector2 (transform.position.x - groundTop.bounds.size.x / 2, transform.position.x + groundTop.bounds.size.x / 2);
		Vector2 verticalBoundary = new Vector2 (transform.position.y + groundTop.bounds.size.y / 2, transform.position.y - groundTop.bounds.size.y / 2);
		float iteratingStartPoint = horizontalBoundary.x + originalGroundTopSize.x / 2;
		float iteratingEndPoint = horizontalBoundary.y - originalGroundTopSize.x / 2;
		float scaledSpriteTopStartPoint = verticalBoundary.x - originalGroundTopSize.y / 2;

		if (originalGroundTopSize.x < groundTop.bounds.size.x) {
			int numberOfNewTiles = (int)(groundTop.bounds.size.x / originalGroundTopSize.x);

			for (int i = 0, l = numberOfNewTiles; i < l; i++) {
				child = Instantiate(childPrefab) as GameObject;
				child.transform.parent = transform;
				child.transform.position = new Vector3(i*originalGroundTopSize.x + iteratingStartPoint, scaledSpriteTopStartPoint, 0);
			}
			// Set the parent last on the prefab to prevent transform displacement
			childPrefab.transform.parent = transform;
			childPrefab.transform.position = new Vector3(iteratingEndPoint, scaledSpriteTopStartPoint, 0);
		}
		// Disable the currently existing sprite component since its now a repeated image
		groundTop.enabled = false;
		groundBottom.transform.position = transform.position;
		groundBottom.transform.localScale = transform.localScale;

		float YScale = (groundTop.bounds.size.y > groundBottom.bounds.size.y) ? (groundBottom.bounds.size.y / groundTop.bounds.size.y) : (groundTop.bounds.size.y / groundBottom.bounds.size.y);
		SpriteRenderer childGroundBottom = Instantiate(groundBottom);
		childGroundBottom.sortingOrder = groundTop.sortingOrder;
		childGroundBottom.transform.parent = transform;
		childGroundBottom.transform.localScale = new Vector3 (1,YScale,1);

		//setting back the rotation to the parent element which will effect child elements as well
		transform.localEulerAngles = new Vector3 (0,0,slopeAngle);
	}
}

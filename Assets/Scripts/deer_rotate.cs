using UnityEngine;
using System.Collections;

public class deer_rotate : MonoBehaviour {

    Animator animator;
    public Transform groundCheckFront, groundCheckRear;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.DrawRay(groundCheckFront.position, -groundCheckFront.transform.up, Color.red);
        RaycastHit2D frontHit = Physics2D.Raycast(groundCheckFront.position, -groundCheckFront.transform.up, 2, whatIsGround.value);

        Debug.DrawRay(groundCheckRear.position, -groundCheckRear.transform.up, Color.yellow);
        RaycastHit2D rearHit = Physics2D.Raycast(groundCheckRear.position, -groundCheckRear.transform.up, 2, whatIsGround.value);

        positionOnTerrain(frontHit, rearHit);
    }

    void positionOnTerrain(RaycastHit2D front, RaycastHit2D rear)
    {
        Vector3 averageNormal = Vector3.up;
        Vector3 averagePoint = (groundCheckFront.position + groundCheckRear.position) / 2; ;
        if (front.collider != null && front.transform != transform)
        {
            //Debug.Log("Front hit something: " + front.collider.tag + " normal: " + front.normal);
            Debug.DrawLine(groundCheckFront.position, groundCheckFront.position + new Vector3(front.normal.x, front.normal.y), Color.green);
            if (rear.collider != null && rear.transform != transform)
            {
                //Debug.Log("Rear hit something: " + rear.collider.tag + " normal: " + rear.normal);
                Debug.DrawLine(groundCheckRear.position, groundCheckRear.position + new Vector3(rear.normal.x, rear.normal.y), Color.magenta);
                averageNormal = (front.normal + rear.normal) / 2;
            }
            else if (rear.collider == null || rear.collider == transform)
            {
                averageNormal = front.normal;
            }
        }
        else if (front.collider == null || front.collider == transform)
        {
            if (rear.collider != null && rear.transform != transform)
            {
                //Debug.Log("Rear hit something: " + rear.collider.tag + " normal: " + rear.normal);
                Debug.DrawLine(groundCheckRear.position, groundCheckRear.position + new Vector3(rear.normal.x, rear.normal.y), Color.magenta);
                averageNormal = rear.normal;
            }
            else if (rear.collider == null || rear.collider == transform)
            {
                return;
            }
        }

        Debug.DrawLine(averagePoint, averagePoint + averageNormal, Color.cyan);

        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, averageNormal);
        if (targetRotation.z > 0.35 || targetRotation.z < -0.35)
        {
            //Debug.Log("Can't turn this much: " + targetRotation.z);
            return;
        }
        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 35);
        transform.rotation = Quaternion.Euler(0, 0, finalRotation.eulerAngles.z);
    }
}

using UnityEngine;
using System.Collections;

public class DeerController : MonoBehaviour {

	public float max_speed = 10f;
	bool facing_right = true;

	Animator animator;

	bool frontOnGround, rearOnGround = false;
	public Transform groundCheckFront, groundCheckRear;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 1000f;
    public float move;
	string idleInQueue;

    public AudioSource rockSource;
    public AudioSource grassSource;

    void Start () {
		animator = GetComponent<Animator> ();
    }

	void FixedUpdate () {
		if (!animator.GetBool(idleInQueue)) {
			setRandomIdleAnimationTrigger ();
		}
		frontOnGround = Physics2D.OverlapCircle (groundCheckFront.position, groundRadius, whatIsGround);
		rearOnGround = Physics2D.OverlapCircle (groundCheckRear.position, groundRadius, whatIsGround);

        animator.SetBool ("ground", frontOnGround || rearOnGround);

		animator.SetFloat ("verticalSpeed", GetComponent<Rigidbody2D>().velocity.y);

		move = Input.GetAxis ("Horizontal");

		animator.SetFloat ("speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * max_speed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facing_right || move < 0 && facing_right) {
			flip ();
		}

        if (move == 0 || (!frontOnGround && !rearOnGround)){
            if (grassSource.isPlaying) grassSource.Stop();
            if (rockSource.isPlaying) rockSource.Stop();
        }
    }

    void OnCollisionStay2D(Collision2D col){
        if (col.gameObject.tag == "Grass" && !grassSource.isPlaying && !rockSource.isPlaying && move != 0 && rearOnGround && frontOnGround){
            grassSource.Play();
        }
        else if (col.gameObject.tag == "Rock" && !grassSource.isPlaying && !rockSource.isPlaying && move != 0 && rearOnGround && frontOnGround){
            rockSource.Play();
        }
    }

    void Update(){
		//TODO Define "Jump" key
		if ((frontOnGround || rearOnGround) && Input.GetButtonDown("Jump")) {
			animator.SetBool ("ground", false);
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));
		}
    }

	void flip() {
		facing_right = !facing_right;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
    
	private void setRandomIdleAnimationTrigger()
	{
		string animationTrigger = "idle" + Random.Range(2,4);
		idleInQueue = animationTrigger;
		animator.SetTrigger(animationTrigger);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovement : MonoBehaviour {


	public Rigidbody2D player;
	public float speed = 1.0f;
	[SerializeField]
    protected float jumpHeight = 0.5f;
	[HideInInspector]
	public bool directionR = true;
    protected Animator animator;
    protected bool isJumping;
    protected bool onLadder;
    protected float gravityStorage;

	// Use this for initialization
	protected virtual void Start () {
		player = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		gravityStorage = player.gravityScale;
		onLadder = false;
	}
		
/*	// Update is called once per frame
	void Update(){
		Debug.Log (jumpCheck);
	}
*/
	void FixedUpdate () {
		//Horizontal moving with it's animation playing
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		if (moveHorizontal != 0.0f) {
			animator.SetFloat ("Player_run", 1.0f);
		} else {
			animator.SetFloat ("Player_run", 0.0f);
		}

		player.velocity = new Vector2 (moveHorizontal * speed, player.velocity.y);

		if (moveHorizontal > 0 && !directionR) {
			Flip ();
		} else if (moveHorizontal < 0 && directionR) {
			Flip ();
		}

		//Jumping and it's animation
		if (Input.GetKey ("space") && isJumping == false) {
			player.velocity = new Vector2 (player.velocity.x,jumpHeight); 
			isJumping = true;
			animator.SetBool("Player_jump", true);
		}

        if (!isJumping)
        {
            animator.SetBool("Player_jump", false);
        }

		//Vertical moving for ladders with it's animation
		float moveVertical = Input.GetAxisRaw ("Vertical");
		if (onLadder) {
			player.gravityScale = 0;
			player.velocity = new Vector2 (player.velocity.x, moveVertical * speed);
			if (moveVertical != 0.0f) {
				animator.SetBool ("Player_climb", true);
			}
		} else if(!onLadder){
			player.gravityScale = gravityStorage;
			animator.SetBool("Player_climb", false);
		}
	}

	//Fliping unit model to direction of moving
	protected virtual void Flip()
	{
		directionR = !directionR;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.CompareTag ("Ground"))
		{
			isJumping = false;
		}
	}
/*		//Checking if not in the air for ability to jump
//		if (collider.gameObject.CompareTag ("Ground"))
//		{
//			isJumping = false;
//			animator.SetBool("Player_jump", false);
//		}
//	}
/*	void OnCollisionExit2D(Collision2D collider)
	{
		if (collider.gameObject.CompareTag ("Ground"))
		{
			isJumping = true;
			animator.SetBool("Player_jump", true);
		}
	}*/

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Get on ladder
		if (collider.gameObject.CompareTag ("Ladder"))
		{
			onLadder = true;
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		//Get off ladder
		if (collider.gameObject.CompareTag ("Ladder"))
		{
			onLadder = false;
		}
	}
}

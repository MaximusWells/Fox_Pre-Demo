using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MOVEMENT WITH RAYCAST COLLISIN DETECT //NOT COMPLITED

public class Movement : MonoBehaviour {

    public Rigidbody2D player;
    public float speed = 1.0f;
    [SerializeField]
    protected float jumpHeight = 0.5f;
    [HideInInspector]
    public bool directionR = true;
    public float minGroundNormalY = 0.65f;

    protected Animator animator;
    protected bool onLadder;
    protected float gravityStorage;

    protected const float minMoveDistance = 0.001f;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected const float shellRadius = 0.01f;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Vector2 unitVelocity;

    // Use this for initialization
    protected virtual void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityStorage = player.gravityScale;
        onLadder = false;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void FixedUpdate()
    {
        Vector2 deltaPosition = player.velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 playerVelocity = moveAlongGround * deltaPosition.x;
        BasicMovement(playerVelocity, false);

        playerVelocity = Vector2.zero;
        //Horizontal moving with it's animation playing
        playerVelocity.x = Input.GetAxisRaw("Horizontal");
        unitVelocity = playerVelocity * speed;
        if (playerVelocity.x != 0.0f)
        {
            animator.SetFloat("Player_run", 1.0f);
        }
        else
        {
            animator.SetFloat("Player_run", 0.0f);
        }

        if (playerVelocity.x > 0 && !directionR)
        {
            Flip();
        }
        else if (playerVelocity.x < 0 && directionR)
        {
            Flip();
        }

        //Jumping and it's animation
        if (Input.GetKey("space") && grounded == true)
        {
            player.velocity = new Vector2(player.velocity.y, jumpHeight);
            BasicMovement(player.velocity, true);
            grounded = false;
            animator.SetBool("Player_jump", true);
        }

        if (grounded)
        {
            animator.SetBool("Player_jump", false);
        }

        //Vertical moving for ladders with it's animation
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (onLadder)
        {
            player.gravityScale = 0;
            player.velocity = new Vector2(player.velocity.y, moveVertical * speed);
            if (moveVertical != 0.0f)
            {
                BasicMovement(player.velocity, true);
                animator.SetBool("Player_climb", true);
            }
        }
        else if (!onLadder)
        {
            player.gravityScale = gravityStorage;
            animator.SetBool("Player_climb", false);
        }
        grounded = false;
    }

    void BasicMovement(Vector2 playerVelocity, bool yMovement)
    {
        float distance = playerVelocity.magnitude;
        if (distance > minMoveDistance)
        {
            int count = player.Cast(playerVelocity, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNoraml = hitBufferList[i].normal;
                if(currentNoraml.y > minGroundNormalY)
                {
                    grounded = true;
                    if(yMovement == true)
                    {
                        groundNormal = currentNoraml;
                        currentNoraml.y = 0;
                    }
                }
                float projection = Vector2.Dot(player.velocity, currentNoraml);
                if (projection < 0)
                {
                    player.velocity = player.velocity - projection * currentNoraml;
                }
                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
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

 /*   void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
*/
    void OnTriggerEnter2D(Collider2D collider)
    {
        //Get on ladder
        if (collider.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        //Get off ladder
        if (collider.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }
}

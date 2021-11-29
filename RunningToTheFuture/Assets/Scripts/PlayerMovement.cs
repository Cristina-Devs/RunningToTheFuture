using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedStore;
    public float speedMultiplier;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMiletoneCountStore;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    private bool stoppedJumping;
    private bool canDoubleJump;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    public GameManager gameManager;

    private Rigidbody2D body;  
    //private Collider2D myCollider;
    private Animator anim;

    void Start()
    {
        //references for ridigbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //myCollider = GetComponent<Collider2D>();
        jumpTimeCounter = jumpTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMiletoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
    }

    void Update()
    {
        //grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // antes ed mover el sprite, si la posicion es mayor q el speed milestone, aumentar speed del jugador
        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            // sin esto se mueve ridiculamente rapido aunque intentes ajustar milestones y velocidad
            // de esta forma se puede ir manteniendo en funcion de la distancia recorrida
            // si el primero es a los 100, el siguiente a los 125, 150... etc para hacerlo más jugable
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
        }

        body.velocity = new Vector2(moveSpeed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                Jump();
                stoppedJumping = false;
            }
            if (!grounded && canDoubleJump)
            {
                Jump();
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
            }
        }

        if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                Jump();
                jumpTimeCounter -= Time.deltaTime;
            }
        }


        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
             jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }


        // anim.setFloat("Speed", body.velocity.x);
        anim.SetBool("jump", !grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        //grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "killbox") 
        {
            gameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMiletoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
        }
        //grounded = true;
    }

    //////if (horizontalInput > 0.01f) //look at right
    //////    transform.localScale = Vector3.one;
    //////else if (horizontalInput < 0.01f) //looking at left
    //////    transform.localScale = new Vector3(-1, 1, 1);

    //if (Input.GetKey(KeyCode.Space))// && grounded)
    //{
    //    anim.SetBool("jump", true);
    //    Jump();

    //}

    //set animator parameters
    //if (grounded) {
    //    anim.SetBool("jump", false);
    //}

    // endless runner  bcause this is call once per physics frame
    //private void FixedUpdate()
    //{
    //    //and here in endless runner body.velocity = new Vector2(0, playerFirection.y * speed);
    //}

    //private void Jump()
    //{
    //    body.velocity = new Vector2(body.velocity.x, jumpForce);
    //    anim.SetTrigger("jump");
    //    //grounded = false;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //if (collision.gameObject.tag == "Ground")
    //        //grounded = true;
    //}
    //Start is called before the first frame update
    //void Start()
    //{

    //}

    //Update is called once per frame
    //void Update()
    //{

    //}
}

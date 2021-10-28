using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; 
    public float jumpForce;
    public float jumpTime;
    public bool grounded;
    public LayerMask whatIsGround;
    public GameManager gameManager;

    private float jumpTimeCounter;
    private Rigidbody2D body;  
    private Collider2D myCollider;
    private Animator anim;

    void Start()
    {
        //references for ridigbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        jumpTimeCounter = jumpTime;
    }

    void Update()
    {
        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        body.velocity = new Vector2(moveSpeed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                Jump();
            }
        }

        if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0))
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
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadMovement : MonoBehaviour
{
    public float movementDistance;
    public float speed;
    public bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void OnEnable()
    {
        leftEdge = transform.position.y - movementDistance;
        rightEdge = transform.position.y + movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            goMoveLeft();
        }
        else
        {
            goMoveRight();
        }

    }

    void goMoveLeft()
    {
        //Debug.Log("-- goMoveLeft ? -- transform.position.x > leftEdge: " + transform.position.x);
        if (transform.position.y > leftEdge)
        {
            //Debug.Log("-- goMoveLeft -- YES");
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            //Debug.Log("-- goMoveLeft -- NOUP");
            movingLeft = false;
        }
    }

    void goMoveRight()
    {
        //Debug.Log("-- goMoveRight ? -- transform.position.x < rightEdge: " + transform.position.x);
        if (transform.position.y < rightEdge)
        {
            //Debug.Log("-- goMoveRight -- YES");
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            //Debug.Log("-- goMoveRight -- NOUP");
            movingLeft = true;
        }
    }
}

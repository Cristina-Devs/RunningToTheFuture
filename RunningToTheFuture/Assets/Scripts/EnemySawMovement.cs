using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySawMovement : MonoBehaviour
{
    /*public float movementDistance;
    public float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        Debug.Log("Starttttt leftEdge: " + leftEdge);
        Debug.Log("Starttttt rightEdge: " + rightEdge);
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
        Debug.Log("-- goMoveLeft ? -- transform.position.x > leftEdge: " + transform.position.x);
        if (transform.position.x > leftEdge)
        {
            Debug.Log("-- goMoveLeft -- YES");
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            Debug.Log("-- goMoveLeft -- NOUP");
            movingLeft = false;
        }
    }

    void goMoveRight()
    {
        Debug.Log("-- goMoveRight ? -- transform.position.x < rightEdge: " + transform.position.x);
        if (transform.position.x < rightEdge)
        {
            Debug.Log("-- goMoveRight -- YES");
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            Debug.Log("-- goMoveRight -- NOUP");
            movingLeft = true;
        }
    }*/
}

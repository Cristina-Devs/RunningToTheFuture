using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySawMovement : MonoBehaviour
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
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
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
        if (transform.position.x > leftEdge)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            movingLeft = false;
        }
    }

    void goMoveRight()
    {
        if (transform.position.x < rightEdge)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            movingLeft = true;
        }
    }
}

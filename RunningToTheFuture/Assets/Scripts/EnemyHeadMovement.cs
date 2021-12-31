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
        if (transform.position.y > leftEdge)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            movingLeft = false;
        }
    }

    void goMoveRight()
    {
        if (transform.position.y < rightEdge)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            movingLeft = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFishGenerator : MonoBehaviour
{
    public ObjectPooler pinkFishPool;
    public ObjectPooler birdPool;
    public ObjectPooler monkeyPool;
    public ObjectPooler turtlePool;
    public float distanceBetweenFishes;

    public void SpawnFishes(Vector3 startPosition) 
    {
        GameObject fish1 = pinkFishPool.GetPooledObject();
        fish1.transform.position = startPosition;
        fish1.SetActive(true);

        GameObject fish2 = pinkFishPool.GetPooledObject();
        fish2.transform.position = new Vector3(startPosition.x - distanceBetweenFishes, startPosition.y, startPosition.z);
        fish2.SetActive(true);

        GameObject fish3 = pinkFishPool.GetPooledObject();
        fish3.transform.position = new Vector3(startPosition.x + distanceBetweenFishes, startPosition.y, startPosition.z);
        fish3.SetActive(true);

    }

    public void addBirdPool(Vector3 startPosition)
    {
        GameObject bird = birdPool.GetPooledObject();
        bird.transform.position = startPosition;
        bird.SetActive(true);
    }
    public void addMonkeyPool(Vector3 startPosition)
    {
        GameObject monkey = monkeyPool.GetPooledObject();
        monkey.transform.position = startPosition;
        monkey.SetActive(true);
    }

    public void addTurtlePool(Vector3 startPosition)
    {
        GameObject turtle = turtlePool.GetPooledObject();
        turtle.transform.position = startPosition;
        turtle.SetActive(true);
    }
}

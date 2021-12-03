using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFishGenerator : MonoBehaviour
{
    public ObjectPooler pinkFishPool;
    //animals
    public ObjectPooler birdPool;
    public ObjectPooler monkeyPool;
    public ObjectPooler turtlePool;

    //fruits
    public ObjectPooler strawberryPool;
    public ObjectPooler melonPool;
    public ObjectPooler bananaPool;
    public ObjectPooler pineapplePool;

    public float distanceBetweenFishes;

    public void SpawnFishes(Vector3 startPosition) 
    {
        print("inside SpawnFishes");
        ObjectPooler pooler = getRandomFruit();

        GameObject fish1 = pooler.GetPooledObject();
        fish1.transform.position = startPosition;
        fish1.SetActive(true);

        GameObject fish2 = pooler.GetPooledObject();
        fish2.transform.position = new Vector3(startPosition.x - distanceBetweenFishes, startPosition.y, startPosition.z);
        fish2.SetActive(true);

        GameObject fish3 = pooler.GetPooledObject();
        fish3.transform.position = new Vector3(startPosition.x + distanceBetweenFishes, startPosition.y, startPosition.z);
        fish3.SetActive(true);

    }

    public ObjectPooler getRandomFruit() {

        int random = UnityEngine.Random.Range(1, 5);
        if (random == 1) {
            return strawberryPool;
        }
        else if (random == 2) {
            return melonPool;
        }
        else if (random == 3) {
            return bananaPool;
        }
        else
        {
            return pineapplePool;
        }
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

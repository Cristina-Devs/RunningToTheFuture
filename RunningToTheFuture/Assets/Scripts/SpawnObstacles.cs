using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject bottleObstacle;
    public float maxX, minX;
    public float timeBetweenSpawn;
    private float spawnTime;

    private float initialY = (float)-3.345;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);

        Instantiate(bottleObstacle, transform.position + new Vector3(randomX, initialY, 0), transform.rotation);
    }
}

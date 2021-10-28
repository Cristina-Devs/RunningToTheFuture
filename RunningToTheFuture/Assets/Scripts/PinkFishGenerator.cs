using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkFishGenerator : MonoBehaviour
{
    public ObjectPooler pinkFishPool;
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
}

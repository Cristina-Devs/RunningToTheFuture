using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] platforms;  //8
    private int platformSelector;
    private float[] platformWidths;
    public ObjectPooler[] objectPools;

    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++) 
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
            Console.WriteLine("platformWidth: " + platformWidths[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = UnityEngine.Random.Range(distanceBetweenMin, distanceBetweenMax);
            platformSelector = UnityEngine.Random.Range(0, objectPools.Length);
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);

            // Instantiate(/*platform*/ objectPools[platformSelector].pooledObject, transform.position, transform.rotation);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}

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

    //flag
    private bool spikeAdded;
    private bool fishesAdded;
    private bool showMonkey;
    private bool monkeyAdded;
    public bool showMonkeyFirstTime;

    //public GameObject[] platforms;  //8
    private int platformSelector;
    private float[] platformWidths;
    public ObjectPooler[] objectPools;

    private float minHeight; //starting point of platforms
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange; //maximo salto para que no aparezcan muy arriba las plataformas
    private float heightChange;

    // fishes
    private PinkFishGenerator pinkFishGenerator;
    public float randomFishTreshold;

    // spikes
    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    // saws
    public float randomSawsThreshold;
    public ObjectPooler spikePoolMovement;
    private ScoreManager scoreManager;
    public float movementDistance;
    public float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private EnemySawMovement enemySawMovement;


    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++) 
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
            //Console.WriteLine("platformWidth: " + platformWidths[i]);
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        pinkFishGenerator = FindObjectOfType<PinkFishGenerator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        enemySawMovement = FindObjectOfType<EnemySawMovement>();

        //initSawMovement();
        //leftEdge = transform.position.x - movementDistance;
        //rightEdge = transform.position.x + movementDistance;
        showMonkey = false;
        showMonkeyFirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        spikeAdded = false;
        if (scoreManager.scoreCount > 1000 && showMonkeyFirstTime) 
            showMonkey = true;

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = UnityEngine.Random.Range(distanceBetweenMin, distanceBetweenMax);
            platformSelector = UnityEngine.Random.Range(0, objectPools.Length);

            setupPlatformsHeight();

            //antes de que se muevan logicamnete
            //transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            // Instantiate(/*platform*/ objectPools[platformSelector].pooledObject, transform.position, transform.rotation);
            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //justo despues de que se haya generado la  nueva plataforma, PERO 
            //justo antes de que el generador se mueva hasta la punta de la plataforma para seguir generando
            //generamos los peces (3), en el centro pero un poco más arriba de la plataforma no en el suelo (1f)
            //y de forma aleatoria, no siempre
            fishesAdded = false;
            monkeyAdded = false;
            if (UnityEngine.Random.Range(0f, 100f) < randomFishTreshold)
            {
                if (showMonkey)
                {
                    pinkFishGenerator.addMonkeyPool(new Vector3(transform.position.x, 2.2f, transform.position.z));
                    showMonkey = false;
                    showMonkeyFirstTime = false;
                    monkeyAdded = true;
                } else {
                    pinkFishGenerator.SpawnFishes(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                    fishesAdded = true;
                }
            }

            // just after some random fishes, we are gonna add some random spikes:
            if (UnityEngine.Random.Range(0f, 100f) < randomSpikeThreshold && !monkeyAdded)
            {
                GameObject newSpike = spikePool.GetPooledObject();
                // to make appear it in the long of the width platform, we use (-3, +3) for instance:
                float spikeXPosition = UnityEngine.Random.Range(-platformWidths[platformSelector] / 2f + 1f, platformWidths[platformSelector] / 2f - 1f);
                // to appear at the top of the platform:
                Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
                spikeAdded = true;

            }

            // Start appearing not only spikes but also saws
            if (scoreManager.scoreCount > 1000 && !spikeAdded && fishesAdded && !monkeyAdded && shouldShowSaw())
            {
                GameObject newSaw = spikePoolMovement.GetPooledObject();
                Vector3 sawPosition = new Vector3(0f, 0.5f, 0f);
                newSaw.transform.position = transform.position + sawPosition;
                newSaw.transform.rotation = transform.rotation;
                newSaw.SetActive(true);
            }

            // finally move the point to create the new platform
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
    }

    void setupPlatformsHeight()
    {
        //poner las plataformas a la altura
        heightChange = transform.position.y + UnityEngine.Random.Range(maxHeightChange, -maxHeightChange); // platforms currently + random value
                                                                                                           //heightChange = transform.position.y + 2; // platforms currently + random value
                                                                                                           // que no aparezcan fuera de los limites
        if (heightChange > maxHeight)
        {
            heightChange = maxHeight;
        }
        else if (heightChange < minHeight)
        {
            heightChange = minHeight;
        }
    }

    bool shouldShowSaw() 
    {
        return UnityEngine.Random.Range(0f, 100f) < randomSawsThreshold;
    }

    /*void goMoveLeft(GameObject newSaw)
    {
        Debug.Log("-- goMoveLeft ? -- transform.position.x > leftEdge: " + transform.position.x);
        if (transform.position.x > leftEdge)
        {
            Debug.Log("-- goMoveLeft -- YES");
            newSaw.transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y + 0.5f, transform.position.z);
        }
        else
        {
            Debug.Log("-- goMoveLeft -- NOUP");
            movingLeft = false;
        }
    }
    
    void goMoveRight(GameObject newSaw)
    {
        Debug.Log("-- goMoveRight ? -- transform.position.x < rightEdge: " + transform.position.x);
        if (transform.position.x < rightEdge)
        {
            Debug.Log("-- goMoveRight -- YES");
            newSaw.transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y + 0.5f, transform.position.z);
        }
        else
        {
            Debug.Log("-- goMoveRight -- NOUP");
            movingLeft = true;
        }
    }*/
}

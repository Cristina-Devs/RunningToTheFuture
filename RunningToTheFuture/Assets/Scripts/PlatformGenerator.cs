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

    // levels of dificulty
    private float easyBarrier = 1000f;
    private float normalBarrier = 2000f;
    private float hardBarrier = 3000f;

    //flag-points
    private bool spikeAdded;
    private bool fishesAdded;
    private bool birdAdded;
    private bool monkeyAdded;
    private bool turtleAdded;

    //flag-triggers
    private bool showBird;
    private bool showMonkey;
    private bool showTurtle;
    public bool showBirdFirstTime;
    public bool showMonkeyFirstTime;
    public bool showTurtleFirstTime;

    //public GameObject[] platforms; 
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
    //private bool movingLeft;
    //private float leftEdge;
    //private float rightEdge;
    //private EnemySawMovement enemySawMovement;

    // Start is called before the first frame update
    void Start()
    {
        platformWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++) 
        {
            platformWidths[i] = objectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        pinkFishGenerator = FindObjectOfType<PinkFishGenerator>();
        scoreManager = FindObjectOfType<ScoreManager>();
        //enemySawMovement = FindObjectOfType<EnemySawMovement>();

        setupFlagsForAnimalsCaged();
    }

    // Update is called once per frame
    void Update()
    {
        spikeAdded = false;
        checkIfAnimalsShouldBeShown();

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
            //generamos los peces (3), en el centro pero un poco m�s arriba de la plataforma no en el suelo (1f)
            //y de forma aleatoria, no siempre
            initializeAnimalsVisibility();
            if (UnityEngine.Random.Range(0f, 100f) < randomFishTreshold)
            {
                showFishesOrCagedAnimals();
            }

            // just after some random fishes, we are gonna add some random spikes:
            if (UnityEngine.Random.Range(0f, 100f) < randomSpikeThreshold)
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
            if (scoreManager.scoreCount > easyBarrier && !spikeAdded && fishesAdded && shouldShowSaw())
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

    void setupFlagsForAnimalsCaged()
    {
        showBird = false;
        showBirdFirstTime = true;
        showMonkey = false;
        showMonkeyFirstTime = true;
        showTurtle = false;
        showTurtleFirstTime = true;
    }

    void checkIfAnimalsShouldBeShown()
    {
        if (scoreManager.scoreCount > easyBarrier && showBirdFirstTime)
            showBird = true;

        if (scoreManager.scoreCount > normalBarrier && showMonkeyFirstTime)
            showMonkey = true;

        if (scoreManager.scoreCount > hardBarrier && showTurtleFirstTime)
            showTurtle = true;
    }

    void initializeAnimalsVisibility()
    {
        fishesAdded = false;
        birdAdded = false;
        monkeyAdded = false;
        turtleAdded = false;
    }

    void showFishesOrCagedAnimals()
    {
        if (showBird)
        {
            pinkFishGenerator.addBirdPool(new Vector3(transform.position.x, 2.2f, transform.position.z));
            showBird = false;
            showBirdFirstTime = false;
            birdAdded = true;
        }
        else if (showMonkey)
        {
            pinkFishGenerator.addMonkeyPool(new Vector3(transform.position.x, 2.5f, transform.position.z));
            showMonkey = false;
            showMonkeyFirstTime = false;
            monkeyAdded = true;
        }
        else if (showTurtle)
        {
            pinkFishGenerator.addTurtlePool(new Vector3(transform.position.x, 2.5f, transform.position.z));
            showTurtle = false;
            showTurtleFirstTime = false;
            turtleAdded = true;
        }
        else
        {
            pinkFishGenerator.SpawnFishes(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            fishesAdded = true;
        }
    }

    bool shouldShowSaw() 
    {
        return UnityEngine.Random.Range(0f, 100f) < randomSawsThreshold;
    }
}

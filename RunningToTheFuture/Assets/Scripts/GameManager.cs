using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerMovement player;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;
    private ScoreManager scoreManager;

    public DeathMenu deathMenu;


    private void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        scoreManager.scoreIncreasing = false;
        player.gameObject.SetActive(false);
        deathMenu.gameObject.SetActive(true);
    }

    public void Reset() 
    {
        deathMenu.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        player.gameObject.SetActive(true);

        scoreManager.scoreCount = 0;
        scoreManager.scoreIncreasing = true;

    }
}

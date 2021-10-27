using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerMovement player;
    private Vector3 playerStartPoint;

    private PlatformDestroyer[] platformList;

    private void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        StartCoroutine("RestartGameCo");
        //player.gameObject.SetActive(false);
        //gameOverPanel.SetActive(true);
        //platformList = FindObjectsOfType<PlatformDestroyer>();
        //for (int i = 0; i < platformList.Length; i++)
        //{
        //    platformList[i].gameObject.SetActive(false);
        //}
    }

    private IEnumerator RestartGameCo()
    {
        player.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        gameOverPanel.SetActive(false);
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        player.gameObject.SetActive(true);
    }

    public void TryAgain() 
    {
        gameOverPanel.SetActive(false);
        player.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        player.gameObject.SetActive(true);

    }
}

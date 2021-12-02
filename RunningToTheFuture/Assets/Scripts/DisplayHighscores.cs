using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
    public Text[] highscoreText;
    Highscores highscoreManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Fetching...";
        }
        highscoreManager = GetComponent<Highscores>();
        StartCoroutine("RefreshHighScores");
    }

    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {  // if we run out of high scores then show the position, otherwise add on high scores
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
            else {
                highscoreText[i].text += "empty";
            }
        }
    }

    IEnumerator RefreshHighScores() 
    {
        while (true) 
        {
            highscoreManager.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }
}

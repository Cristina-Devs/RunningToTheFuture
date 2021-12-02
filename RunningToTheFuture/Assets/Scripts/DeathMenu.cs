using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;
    private ScoreManager scoreManager;
    public Text pointsText;

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void Update()
    {
        pointsText.text = "You got " + Mathf.Round(scoreManager.scoreCount) + " points!";
    }

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
        saveScoreInPlayerPrefs();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuLevel);
        saveScoreInPlayerPrefs();
    }
    public void SaveDataInRanking()
    {
        Highscores.AddNewHighScore("prueba", 123);
        saveScoreInPlayerPrefs();
    }

    public void saveScoreInPlayerPrefs()
    {
        float currentScore = scoreManager.highScoreCount;
        PlayerPrefs.SetFloat("savedScore", currentScore);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public string mainMenuLevel;
    private ScoreManager scoreManager;

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
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

    public void saveScoreInPlayerPrefs()
    {
        float currentScore = scoreManager.highScoreCount;
        PlayerPrefs.SetFloat("savedScore", currentScore);
    }

}

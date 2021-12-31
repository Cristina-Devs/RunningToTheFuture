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
    public InputField inputField;
    public Button saveDataButton;

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void Update()
    {
        float animals = Mathf.Round(scoreManager.animalsCount);

        if (animals == 1) {
            pointsText.text = "Congrats: " + Mathf.Round(scoreManager.scoreCount) + " points  and " + animals + "/3 animals rescued!";
        } else if (animals == 2)
        {
            pointsText.text = "Goood: " + Mathf.Round(scoreManager.scoreCount) + " points  and " + animals + "/3 animals rescued!";
        }
        else 
        {
            pointsText.text = "Wooow: " + Mathf.Round(scoreManager.scoreCount) + " points  and " + animals + "/3 animals rescued!";
        }
    }

    public void OnEnable()
    {
        inputField.interactable = true;
        saveDataButton.interactable = true;
    }

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
        scoreManager.saveScoreInPlayerPrefs();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(mainMenuLevel);
        GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().PlayMusicIfWasPlaying();
        scoreManager.saveScoreInPlayerPrefs();
    }
    public void SaveDataInRanking()
    {
        inputField.interactable = false;
        saveDataButton.interactable = false;
        Highscores.AddNewHighScore(inputField.text, (int)Mathf.Round(scoreManager.scoreCount));
        scoreManager.saveScoreInPlayerPrefs();
    }

    public void saveScoreInPlayerPrefs()
    {
        float currentScore = scoreManager.highScoreCount;
        scoreManager.saveScoreInPlayerPrefs();
    }

}

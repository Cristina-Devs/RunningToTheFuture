using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public float scoreCount;
    public Text highScoreText;
    public float highScoreCount;
    public float pointsPerSecond;
    public bool scoreIncreasing;
    public float savedScore;

    // Start is called before the first frame update
    void Start()
    {
        highScoreCount = getScoreFromPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreIncreasing) 
        { 
            scoreCount += pointsPerSecond * Time.deltaTime;
        }

        if (scoreCount > highScoreCount) 
        {
            highScoreCount = scoreCount;
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void AddScore(int pointsToAdd) 
    {
        scoreCount += pointsToAdd;
    }

    public float getScoreFromPlayerPrefs() 
    {
        savedScore = PlayerPrefs.GetFloat("savedScore");
        return savedScore;
    }
}

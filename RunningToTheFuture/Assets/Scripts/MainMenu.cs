using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;
    public string onBoarding;
    private bool firstTime = true;
    public Button buttonInfo;

    public void PlayGame()
    {
        //if first time, open onboarding, 
        if (firstTime)
        {
            SceneManager.LoadScene(onBoarding);
        }
        else 
        {
            SceneManager.LoadScene(playGameLevel);
        }
    }    
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void InfoTapped()
    {
        SceneManager.LoadScene(onBoarding);
    }
    public void MusicTapped()
    {
    }
    public void SoundsTapped()
    {
    }
    public void RankingTapped()
    {
        Application.Quit();
    }

}

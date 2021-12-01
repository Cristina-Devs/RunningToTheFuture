using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGameLevel;
    public string onBoarding;
    private bool firstTime = true;

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

}

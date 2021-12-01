using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBoarding : MonoBehaviour
{
    public string playGameLevel;

    public void PlayGame()
    {
        //if first time, open onboarding, 
         SceneManager.LoadScene(playGameLevel);
    }

}

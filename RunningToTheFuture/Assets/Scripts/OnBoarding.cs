using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBoarding : MonoBehaviour
{
    public string playGameLevel;
    public string mainScene;

    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().StopMusic();
        SceneManager.LoadScene(playGameLevel);
    }

    public void BackGame()
    {
        SceneManager.LoadScene(mainScene);
    }
}

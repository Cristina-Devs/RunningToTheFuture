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
    public Button buttonMusic;
    public bool soundActive;
    public Sprite enabledMusicSprite;
    public Sprite disabledMusicSprite;

    public void Start()
    {
        //soundActive = intToBool(PlayerPrefs.GetInt("soundActive"));
        updateMusicButtonImage();
    }

    public void Update()
    {
        updateMusicButtonImage();
    }

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
        ToggleSound();
    }

    public void RankingTapped()
    {
        Application.Quit();
    }
    public void ToggleSound()
    {
        soundActive = !soundActive;
        //PlayerPrefs.SetInt("soundActive", boolToInt(soundActive));
        updateMusicButtonImage();

    }

    public void updateMusicButtonImage()
    {
        if (soundActive)
        {
            buttonMusic.gameObject.GetComponent<Image>().sprite = enabledMusicSprite;
            AudioListener.volume = 1f;
        }
        else
        {
            buttonMusic.gameObject.GetComponent<Image>().sprite = disabledMusicSprite;
            AudioListener.volume = 0f;
        }
    }
    
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}

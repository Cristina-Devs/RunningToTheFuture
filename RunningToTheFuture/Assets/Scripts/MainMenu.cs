using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Scenes to navigate 
    public string playGameLevel;
    public string onBoarding;
    public string leaderBoard;

    // Flag to show onboarding first time
    private bool firstTime = true;

    // Sound button
    public Button buttonMusic;
    //public bool soundActive;
    public Sprite enabledMusicSprite;
    public Sprite disabledMusicSprite;

    public void Start()
    {
        //GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().PlayMusic();
    }

    public void Update()
    {
        bool soundActive = GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().IsPlayingMusic();
        updateMusicButtonImage(soundActive);
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
            GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().StopMusic();
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
        SceneManager.LoadScene(leaderBoard);
    }
    
    public void ToggleSound()
    {
        bool soundActive = GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().IsPlayingMusic();
        soundActive = !soundActive;
        updateMusicButtonImage(soundActive);
    }
    
    public void updateMusicButtonImage(bool soundActive)
    {
        if (soundActive)
        {
            GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().PlayMusic();
            buttonMusic.gameObject.GetComponent<Image>().sprite = enabledMusicSprite;
            AudioListener.volume = 1f;
        }
        else
        {
            GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().StopMusic();
            buttonMusic.gameObject.GetComponent<Image>().sprite = disabledMusicSprite;
            AudioListener.volume = 0f;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuLevel;
    public GameObject pauseMenu;

    public void PauseGame()
    {
        //SceneManager.LoadScene(pauseMenuLevel);
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        FindObjectOfType<GameManager>().Reset();
        //GameObject.FindGameObjectWithTag("MusicRunning").GetComponent<MusicClass>().RestartMusic();
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuLevel);
        GameObject.FindGameObjectWithTag("MusicIntro").GetComponent<MusicClass>().PlayMusicIfWasPlaying();
    }
}

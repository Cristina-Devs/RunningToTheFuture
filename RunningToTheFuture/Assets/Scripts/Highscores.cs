using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class Highscores : MonoBehaviour
{
    //URL: https://dreamlo.com/lb/F4NJ7MEpM0auweJ_CCgvOwDGBADrlFOkSCjnmbZTZSFw
    const string privateCode = "F4NJ7MEpM0auweJ_CCgvOwDGBADrlFOkSCjnmbZTZSFw";
    const string publicCode = "61a8b57d8f418f1278e49de1";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    static Highscores instance;
    DisplayHighscores highscoresDisplay;

    public string mainScene;

    private void Awake()
    {
        //clearScores();
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
        //AddNewHighScore("Cristina", 0);
        //AddNewHighScore("pepe", 0);
        //DownloadHighScores();
    }

    public static void AddNewHighScore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    void clearScores() {
        //http://dreamlo.com/lb/F4NJ7MEpM0auweJ_CCgvOwDGBADrlFOkSCjnmbZTZSFw/clear
        WWW www = new WWW(webURL + privateCode + "/clear");
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        //UnityWebRequest www = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) 
        {
            print("Upload Successful");
            DownloadHighScores();
        }
        else {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighScores() 
    {
        StartCoroutine("DownloadHighscoreFromDatabase");
    }

    IEnumerator DownloadHighscoreFromDatabase()
    {
        //UnityWebRequest www = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Downloading: " + www.error);
        }
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(mainScene);
    }
}

public struct Highscore {
    public string username;
    public int score;

    public Highscore(string _username, int _score) {
        username = _username;
        score = _score;
    }
}



/*
 WWW myWww = new WWW("http://www.myserver.com/foo.txt");
// ... is analogous to ...
UnityWebRequest myWr = UnityWebRequest.Get("http://www.myserver.com/foo.txt");
 */

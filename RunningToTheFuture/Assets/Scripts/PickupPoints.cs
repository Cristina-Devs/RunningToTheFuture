using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPoints : MonoBehaviour
{
    public int scoreToGive;
    private ScoreManager scoreManager;

    private AudioSource fishSound;
    private AudioSource animalRescueSound;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        fishSound = GameObject.Find("FishSound").GetComponent<AudioSource>();
        animalRescueSound = GameObject.Find("AnimalSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            scoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
            if (scoreToGive > 100)
            {
                scoreManager.AddAnimalRescued(1);
                if (animalRescueSound.isPlaying) { animalRescueSound.Stop(); }
                animalRescueSound.Play();
            }
            else
            {
                if (fishSound.isPlaying) { fishSound.Stop(); }
                fishSound.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreSceneManager : MonoBehaviour
{
    public Text score;
    public Text highScore;
    int _score;
    int _highScore;
    AudioSource _audioSource;
    public AudioClip _clickSound;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _score = 0;
        if (PlayerPrefs.HasKey("Score")) {
            _score = PlayerPrefs.GetInt("Score");    
        }
        score.text = "Score: " + _score;

        _highScore = 0;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            _highScore = PlayerPrefs.GetInt("HighScore");
        }
        highScore.text = "HighScore: " + _highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTitleScene()
    {
        _audioSource.PlayOneShot(_clickSound);
        //deletes all stored data about that run, still saving HighScore
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Gamemode");
        SceneManager.LoadScene("TitleScene");
    }

    public void PlayAgain()
    {
        _audioSource.PlayOneShot(_clickSound);
        PlayerPrefs.DeleteKey("Score");
        SceneManager.LoadScene("Level1");
    }
}

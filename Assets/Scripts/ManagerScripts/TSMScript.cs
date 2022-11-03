using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TSMScript : MonoBehaviour
{
    public Text _highScoreText;
    int oldVal = 0;
    AudioSource _audioSource;
    public AudioClip _buttonClickSound;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            _highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            _highScoreText.text = "High Score: 0";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onStartButtonClick(int gameModeIndex)
    {
        _audioSource.PlayOneShot(_buttonClickSound);
        switch (gameModeIndex) {
            case 0:
                //standard hardcore mode
                PlayerPrefs.SetInt("Gamemode", 0);
                break;
            case 1:
                //explore mode
                PlayerPrefs.SetInt("Gamemode", 1);
                break;
        }
        SceneManager.LoadScene("StoryScene");
    }
}

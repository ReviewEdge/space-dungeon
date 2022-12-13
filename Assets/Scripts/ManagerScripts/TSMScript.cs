using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TSMScript : MonoBehaviour
{
    public Text _highScoreText;
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

    public void onStartButtonClick()
    {
        _audioSource.PlayOneShot(_buttonClickSound);
        SceneManager.LoadScene("StoryScene");
    }

    public void onHowToPlayButtonClick() 
    {
        _audioSource.PlayOneShot(_buttonClickSound);
        SceneManager.LoadScene("InstructionsScene");
    }
}

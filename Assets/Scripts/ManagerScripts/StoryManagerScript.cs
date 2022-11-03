using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManagerScript : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip _buttonClickSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSkipButtonClick()
    {
        _audioSource.PlayOneShot(_buttonClickSound);

        SceneManager.LoadScene("Level1");
    }
}

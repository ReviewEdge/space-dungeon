using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ISManagerScript : MonoBehaviour
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

    public void onBackButtonClick()
    {
        _audioSource.PlayOneShot(_buttonClickSound);

        SceneManager.LoadScene("TitleScene");
    }
}

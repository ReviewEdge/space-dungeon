using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumPadScript : MonoBehaviour
{
    public GameObject cellDoor;
    public PrisonerScript prisoner;
    private bool _isHacking;
    private MeshRenderer _popupText;
    public Image _progressBar;
    private Canvas _canvas;
    private GeneralManagerScript _generalManager;
    private int unlockTime = 2;
    private float _progressBarTime = 0;
    private AudioSource _audioSource;
    public AudioClip _deactivateSound;

    // Start is called before the first frame update
    void Start()
    {
        _isHacking = false;
        _popupText = GetComponentInChildren<MeshRenderer>();
        _canvas = GetComponentInChildren<Canvas>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHacking && !Input.GetKey(KeyCode.E)) {
            _isHacking = false;
            _canvas.enabled = false;
            _progressBarTime = 0;
            _audioSource.Stop();
        }
        if (_isHacking) {
            _progressBar.fillAmount = _progressBarTime/unlockTime;
            _progressBarTime += Time.deltaTime;
            if (_progressBarTime >= unlockTime) {
                OnFinishHack();
            }
        }
    }

    public void StartHack() {
        if (_isHacking && this.enabled) {
            return;
        }

        _audioSource.Play();
        _canvas.enabled = true;
        _isHacking = true;
    }

    private void OnFinishHack()
    {
        if (!_isHacking)
        {
            return;
        }

        _canvas.enabled = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_deactivateSound);
        Destroy(cellDoor);
        _generalManager.IncrementScore(1000);
        _generalManager.FreePrisoner();
        prisoner.Freed();
        _popupText.enabled = false;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == TagList.playerTag && this.enabled) 
        {
            _popupText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == TagList.playerTag)
        {
            _popupText.enabled = false;
            _isHacking = false;
        }
    }
}

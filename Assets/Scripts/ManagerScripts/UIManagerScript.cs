using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManagerScript : MonoBehaviour
{
    public GameObject _player;
    
    public Text levelText;
    public Text scoreText;
    public Text freedText;
    public Text healthText;
    public Text ammoText;

    void Start()
    {   
        _player = GameObject.FindWithTag(TagList.playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText(_player.GetComponent<PlayerScript>().health);
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateFreedText(int remainingPrisoners, int totalPrisoners)
    {
        int numofRescuedPrisoners = totalPrisoners - remainingPrisoners;
        freedText.text = numofRescuedPrisoners + " out of " + totalPrisoners + " rescued";
    }
    public void UpdateHealthText(int health) {
        healthText.text = "Health: " + health;
    }
    public void UpdateAmmoText(int magazineAmmo, int remainingAmmo) {
        ammoText.text = magazineAmmo + " / " + remainingAmmo; 
    }
    public void UpdateAmmoText() {
        ammoText.text = "- / -";
    }
    
}

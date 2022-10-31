using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManagerScript : MonoBehaviour
{
    private PlayerScript _player;
    
    public Text levelText;
    public Text scoreText;
    public Text freedText;
    public Text healthText;
    public Text ammoText;
    public Text livesText;

    void Start()
    {   
        GameObject player = GameObject.FindWithTag(TagList.playerTag);
        _player = player.GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText(_player.health);
        UpdateLivesText(_player.lives);
        UpdateAmmoText(_player.magazineAmmo, _player.remainingAmmo);
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
        if(health <= 0){
            healthText.text = "Health: 0";
        } else 
        {
            healthText.text = "Health: " + health;
        }
    }
    public void UpdateAmmoText(int magazineAmmo, int remainingAmmo) {
        if (magazineAmmo >= 0 && remainingAmmo >= 0)
        {
            ammoText.text = "- / -";
        }
        else
        {
            ammoText.text = magazineAmmo + " / " + remainingAmmo;
        }
    }
    public void UpdateLivesText(int livesInput)
    {
        //livesText.text = "Lives: " + livesInput;
    }
    
}

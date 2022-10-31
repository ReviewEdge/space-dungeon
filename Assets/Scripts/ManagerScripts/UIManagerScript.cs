using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    private PlayerScript _player;
    
    public Text levelText;
    public Text scoreText;
    public Text freedText;
    public Image healthImage;
    public Text ammoText;

    void Start()
    {   
        GameObject player = GameObject.FindWithTag(TagList.playerTag);
        _player = player.GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText(_player.health);
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
        healthImage.fillAmount = health/100f;
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
    
}

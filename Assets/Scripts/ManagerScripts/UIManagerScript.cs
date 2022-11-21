using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    private PlayerScript _player;
    private GeneralManagerScript _generalManager;
    public Text levelText;
    public Text scoreText;
    public Text freedText;
    public Image healthImage;
    public Text ammoText;
    public Sprite[] weaponSprites;
    public Image primaryWeaponImage;
    public Image secondayWeaponImage;

    void Start()
    {   
        GameObject player = GameObject.FindWithTag(TagList.playerTag);
        _player = player.GetComponent<PlayerScript>();
        _generalManager = FindObjectOfType<GeneralManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthImage(_player.health);
        // UpdateLivesText(_player.lives);
        
        UpdateAmmoText(_player.magazineAmmo, _player.remainingAmmo);
        UpdateScoreText(_generalManager.score);
        UpdateFreedText(_generalManager.remainingPrisoners, _generalManager.numofPrisoners);
        UpdateWeaponImage(_player.weapon);
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
    public void UpdateHealthImage(int health) {
        healthImage.fillAmount = health/100f;
    }
    public void UpdateAmmoText(int magazineAmmo, int remainingAmmo) {
        if (magazineAmmo >= 0 && remainingAmmo >= 0)
        {
            ammoText.text = "Ammo: " + remainingAmmo;
        }
        else
        {
            ammoText.text = magazineAmmo + " / " + remainingAmmo;
        }
    }

    public void UpdateWeaponImage(TagList.weaponType weapon) {
        switch (weapon)
        {
            case TagList.weaponType.LaserSword:
                primaryWeaponImage.sprite = weaponSprites[0];
                secondayWeaponImage.sprite = weaponSprites[1];
                break;
            case TagList.weaponType.RayGun:
                primaryWeaponImage.sprite = weaponSprites[1];
                secondayWeaponImage.sprite = weaponSprites[0];
                break;
        }
    }
}

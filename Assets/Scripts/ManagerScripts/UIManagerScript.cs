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
    public Image[] weaponFrames;
    public Image[] weaponImages;
    public Sprite[] weaponSprites;

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
        
        UpdateAmmoText(_player.currentWeapon.ammo);
        UpdateScoreText(_generalManager.score);
        UpdateFreedText(_generalManager.remainingPrisoners, _generalManager.numofPrisoners);
        UpdateWeaponImages(_player.weapons, _player.currentWeapon);
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
    public void UpdateAmmoText(int ammo) {
        ammoText.text = "Ammo: " + ammo;
    }

    public void UpdateWeaponImages(Weapon[] weapons, Weapon currentWeapon) {

        for (int i = 0; i < weapons.Length; i++)
        {
            //highlight selected weapon
            weaponFrames[i].color = Color.white;
            if (weapons[i].Equals(currentWeapon))
            {
                weaponFrames[i].color = Color.gray;
            }

            //fill images with weapins
            weaponImages[i].sprite = weaponSprites[(int)weapons[i].weaponType];
        }
    }
}

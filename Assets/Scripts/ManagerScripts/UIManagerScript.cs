using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    private PlayerScript _player;
    private GeneralManagerScript _generalManager;
    public Text levelText;
    public Text scoreText;
    public Text livesText;
    public Text prisonersText;
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
        if (SceneManager.GetActiveScene().name.Equals(TagList.finalLevel))
        {
            prisonersText.text = "Escape to the Ship!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthImage(_player.health);
        UpdatePlayerPrisonerText(_generalManager.playerLives);
        if (!SceneManager.GetActiveScene().name.Equals(TagList.finalLevel))
        {
            UpdatePrisonerText(_generalManager.remainingPrisoners, _generalManager.numofPrisoners);
        }
        UpdateScoreText(_generalManager.score);
        if (_player.currentWeapon != null) {
            UpdateAmmoText(_player.currentWeapon.ammo);
            UpdateWeaponImages(_player.weapons, _player.currentWeapon);
        }
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level;
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdatePrisonerText(int remainingPrisoners, int numofPrisoners) 
    {
        prisonersText.text = numofPrisoners - remainingPrisoners + " out of " + numofPrisoners + " rescued";
    }
    public void UpdatePlayerPrisonerText(int playerLives)
    {
        livesText.text = "Lives: " + playerLives;
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
            weaponFrames[i].color = Color.gray;
            if (weapons[i].Equals(currentWeapon))
            {
                weaponFrames[i].color = Color.white;
            }

            //fill images with weapons
            weaponImages[i].sprite = weaponSprites[(int)weapons[i].weaponType];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManagerScript UIManager;

    int level;
    int score;
    int numofPrisoners;
    int remainingPrisoners;
    int health;
    int magazineAmmo;
    int remainingAmmo;

    void Start()
    {
        level = 1;
        score = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUITextElements();
    }

    private void UpdateUITextElements() {
        UIManager.UpdateLevelText(level);
        UIManager.UpdateScoreText(score);
        UIManager.UpdateFreedText(remainingPrisoners, numofPrisoners);
        UIManager.UpdateHealthText(health);
        if (magazineAmmo == 0 && remainingAmmo == 0) {
            UIManager.UpdateAmmoText();
        } else {
            UIManager.UpdateAmmoText(magazineAmmo, remainingAmmo);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManagerScript UIManager;
    public PlayerScript Player;

    public int level;
    public int numofPrisoners;
    public int score;
    public int remainingPrisoners;
    public GameObject _enemyExplosionPrefab;
    private int _gamemode;

    void Start()
    {
        Player = GameObject.FindWithTag(TagList.playerTag).GetComponent<PlayerScript>();
        level = SceneManager.GetActiveScene().buildIndex;
        numofPrisoners = GameObject.FindGameObjectsWithTag(TagList.PrisonerTag).Length;
        SetPrisoners(numofPrisoners);
        UIManager.UpdateLevelText(level);
        if (PlayerPrefs.HasKey("Gamemode"))
        {
            _gamemode = PlayerPrefs.GetInt("Gamemode");
        }
        else {
            //set to explore if gamemode is unselected, used for dev stuff
            _gamemode = 1;
        }
        LoadStoredData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPrisoners(int prisoners) {
        remainingPrisoners = prisoners;
    }
    public void FreePrisoner() {
        remainingPrisoners--;
        if (remainingPrisoners == 0) {
            LoadNextLevel();
        }
    }

    public void IncrementScore(int points) {
        score += points;
    }

    public void LoadNextLevel() {
        if (SceneManager.sceneCountInBuildSettings > level + 1)
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("Ammo", Player.remainingAmmo);
            PlayerPrefs.SetInt("Weapon", ((int)Player.weapon));
            SceneManager.LoadScene("Level" + (level + 1));
        }
        else
        {
            GameOver();
        }
    }

    public void EnemyDeath(Vector3 spot)
    {
        Instantiate(_enemyExplosionPrefab, spot, Quaternion.identity);
    }

    public void GameOver() {
        if (_gamemode == 1)
        {
            if (level == 5 && remainingPrisoners == 0) //if last level
            {
                int prevHighScore = 0;
                if (PlayerPrefs.HasKey("HighScore"))
                {
                    prevHighScore = PlayerPrefs.GetInt("HighScore");
                }
                //deletes all stored data about that run, still saving HighScore
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("HighScore", prevHighScore);
                SceneManager.LoadScene("TitleScene");
            }
            // auto respawns for testing purposes
            // and explore gamemode

            Player.RespawnPlayer();
        }
        else if (_gamemode == 0)
        {

            //Some code here can be moved when we add the countinue Screen
            int prevHighScore = 0;
            if (PlayerPrefs.HasKey("HighScore"))
            {
                prevHighScore = PlayerPrefs.GetInt("HighScore");
            }

            //deletes all stored data about that run, still saving HighScore
            PlayerPrefs.DeleteAll();

            if (prevHighScore < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                PlayerPrefs.SetInt("HighScore", prevHighScore);
            }

            SceneManager.LoadScene("TitleScene");
        }
    }

    private void LoadStoredData()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
        }
        if (PlayerPrefs.HasKey("Ammo"))
        {
            Player.remainingAmmo = PlayerPrefs.GetInt("Ammo");
        }
        else
        {
            Player.remainingAmmo = 0;
        }
        if (PlayerPrefs.HasKey("Weapon"))
        {
            int test = ((int)Player.weapon);
            if (((int)TagList.weaponType.LaserSword) == test) {
            
            }
            switch (PlayerPrefs.GetInt("Weapon")) {
                case ((int)TagList.weaponType.LaserSword):
                    Player.weapon = TagList.weaponType.LaserSword;
                    break;
                case ((int)TagList.weaponType.RayGun):
                    Player.weapon = TagList.weaponType.RayGun;
                    break;
            }
        }
        else
        {
            Player.remainingAmmo = 0;
        }
    }
}

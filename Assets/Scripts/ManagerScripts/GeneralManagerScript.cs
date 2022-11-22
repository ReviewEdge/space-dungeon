using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManagerScript : MonoBehaviour
{
    UIManagerScript UIManager;
    PlayerScript Player;

    public int level;
    public int numofPrisoners;
    public int score;
    public int remainingPrisoners;
    public GameObject _enemyExplosionPrefab;
    private int _gamemode;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManagerScript>();
        Player = FindObjectOfType<PlayerScript>();
        level = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
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
        LoadScore();
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
/*            PlayerPrefs.SetInt("Weapon", (int)Player.weapon);
            PlayerPrefs.SetInt("Ammo", Player.remainingAmmo);*/
            PlayerPrefs.SetInt("Health", Player.health);
            SceneManager.LoadScene("Level" + (level + 1));
        }
        else
        {
            GameOver();
        }
    }

    public void EnemyDeath(Vector3 spot)
    {
        GameObject enemyDeath = Instantiate(_enemyExplosionPrefab, spot, Quaternion.identity);
        Destroy(enemyDeath, 1);
    }

    public void GameOver() {
        PlayerPrefs.DeleteKey("Weapon");
        PlayerPrefs.DeleteKey("Ammo");
        PlayerPrefs.DeleteKey("Health");

        PlayerPrefs.SetInt("Score", score);
        if (_gamemode == 1)
        {
            if (level == 10 && remainingPrisoners == 0) //if last level
            {
                SceneManager.LoadScene("ScoreScene");
            }

            Player.RespawnPlayer();
        }
        else if (_gamemode == 0)
        {
            if (GetHighScore() < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            SceneManager.LoadScene("ScoreScene");
        }
    }

    private void LoadScore()
    {
        score = 0;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
    }

    private int GetHighScore() {
        int prevHighScore = 0;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            prevHighScore = PlayerPrefs.GetInt("HighScore");
        }
        return prevHighScore;
    }
}

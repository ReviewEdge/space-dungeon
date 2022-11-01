using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManagerScript UIManager;

    public int level;
    public int numofPrisoners;
    public int score;
    public int remainingPrisoners;

    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
        }
        numofPrisoners = GameObject.FindGameObjectsWithTag(TagList.PrisonerTag).Length;
        SetPrisoners(numofPrisoners);
        UIManager.UpdateLevelText(level);
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
            SceneManager.LoadScene("Level" + (level + 1));
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void GameOver() {
        //This code here can be moved when we add the countinue Screen
        if (!PlayerPrefs.HasKey("HighScore") || (PlayerPrefs.HasKey("HighScore") && PlayerPrefs.GetInt("HighScore") < score))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        PlayerPrefs.DeleteKey("Score");
        SceneManager.LoadScene("TitleScene");
    }
}

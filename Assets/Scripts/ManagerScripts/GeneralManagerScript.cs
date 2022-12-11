using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManagerScript : MonoBehaviour
{
    UIManagerScript UIManager;
    PlayerScript Player;
    DoorScript door;

    public int level;
    public int numofPrisoners;
    public int remainingPrisoners;
    public int score;
    public GameObject _enemyExplosionPrefab;
    public int playerLives = 0;
    public GameObject pauseMenu;
    public bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = FindObjectOfType<UIManagerScript>();
        Player = FindObjectOfType<PlayerScript>();
        door = FindObjectOfType<DoorScript>();
        level = int.Parse(SceneManager.GetActiveScene().name.Substring(5));
        numofPrisoners = GameObject.FindGameObjectsWithTag(TagList.PrisonerTag).Length;
        SetPrisoners(numofPrisoners);
        UIManager.UpdateLevelText(level);
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused)
            {
                UnPauseGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void SetPrisoners(int prisoners) {
        remainingPrisoners = prisoners;
    }
    public void FreePrisoner() {
        remainingPrisoners--;
        playerLives++;

        if (remainingPrisoners == 0) {
            UnlockDoor();
        }
    }

    public void IncrementScore(int points) {
        score += points;
    }

    public void UnlockDoor() 
    {
        door.Open();
    }

    public void LoadNextLevel() {

        if (!SceneManager.GetActiveScene().name.Equals("Level10"))
        {
            SavePlayerData(Player.weapons, Player.currentWeapon);

            SceneManager.LoadScene("Level" + (level + 1));
        }
        else 
        {
            LoadScoreScene();
        }
    }

    public void EnemyDeath(Vector3 spot)
    {
        GameObject enemyDeath = Instantiate(_enemyExplosionPrefab, spot, Quaternion.identity);
        Destroy(enemyDeath, 1);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);

        playerLives--;
        PlayerPrefs.DeleteKey("Weapon");
        PlayerPrefs.DeleteKey("Ammo");
        PlayerPrefs.DeleteKey("Health");

        PlayerPrefs.SetInt("Score", score);

        if (playerLives < 0)
        {
            LoadScoreScene();
        }
        else 
        {
            Player.RespawnPlayer();
        }
    }


    private void LoadScoreScene() 
    {
        ClearPlayerData();

        SceneManager.LoadScene("ScoreScene");
    }

    /*
     * Clears all player data except highscore
     */
    private void ClearPlayerData() 
    {
        int highscore = GetHighScore();

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("HighScore", highscore);
    }
    private void SavePlayerData(Weapon[] weapons, Weapon selectedWeapon) 
    {
        int selectedWeaponIndex = 0;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Lives", playerLives);
        PlayerPrefs.SetInt("Health", Player.health);

        for (int i = 0; i < weapons.Length; i++) {
            if (weapons[i].Equals(selectedWeapon)) 
            {
                selectedWeaponIndex = i;
            }
            PlayerPrefs.SetInt("Weapon" + (i + 1), (int)weapons[i].weaponType);
            PlayerPrefs.SetInt("Ammo" + (i+1), weapons[i].ammo);
        }
        PlayerPrefs.SetInt("selectedWeaponIndex", selectedWeaponIndex);
    }

    private void LoadPlayerData()
    {
        score = 0;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }

        playerLives = 0;
        if (PlayerPrefs.HasKey("Lives"))
        {
            playerLives = PlayerPrefs.GetInt("Lives");
        }
    }

    /**
     * Sets the highscore and returns the score
     */
    private int GetHighScore()
    {
        int highScore = 0;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        return highScore;
    }

    public void PauseGame() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gamePaused = true;
    }

    public void UnPauseGame() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gamePaused = false;
    }

    public void LoadMenuScene()
    {
        ClearPlayerData();
        PlayerPrefs.DeleteKey("Score");
        Time.timeScale = 1;

        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame() { 
        ClearPlayerData();
        PlayerPrefs.DeleteKey("Score");
        Application.Quit();
    }
}

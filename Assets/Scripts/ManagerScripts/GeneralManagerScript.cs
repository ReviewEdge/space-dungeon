using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManagerScript UIManager;
    public PlayerScript _player;

    int level;
    int score;
    int numofPrisoners;
    int remainingPrisoners;

    void Start()
    {
        level = 1;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUITextElements();
    }

    private void UpdateUITextElements()
    {
        UIManager.UpdateLevelText(level);
        UIManager.UpdateScoreText(score);
        UIManager.UpdateFreedText(remainingPrisoners, numofPrisoners);
    }

    public void FreePrisoner() {
        remainingPrisoners--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public UIManagerScript UIManager;
    public PlayerScript _player;

    public int level;
    int numofPrisoners;
    int score;
    int remainingPrisoners;

    void Start()
    {
        score = 0;

        numofPrisoners = GameObject.FindGameObjectsWithTag(TagList.PrisonerTag).Length;

        SetPrisoners(numofPrisoners);
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

    public void SetPrisoners(int prisoners) {
        remainingPrisoners = prisoners;
    }
    public void FreePrisoner() {
        remainingPrisoners--;
    }
}

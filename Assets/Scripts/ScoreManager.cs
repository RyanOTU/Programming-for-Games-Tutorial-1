using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    public TextMeshProUGUI scoreUI;
    int totalScore = 0;

    private void Awake()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
        }
        scoreUI.text = "Score: 0";
    }
    public void UpdateScore(int score)
    {
        totalScore += score;
        scoreUI.text = "Score: " + totalScore.ToString();
    }
}

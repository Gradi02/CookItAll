using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Stars")]
    public float starScore3;
    public float starScore2;
    public float starScore1;
    private float currentScore = 0;

    [Header("Time")]
    public float levelLengthInMinutes;
    public float levelLengthInSecond;
    private float levelLength = 0;

    [Header("Refs")]
    private Tasks tasks;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI starText;

    private void Awake()
    {
        levelLength = levelLengthInSecond + levelLengthInMinutes * 60;
        tasks = GetComponent<Tasks>();
    }

    private void Update()
    {
        if(Time.time > levelLength && levelLength > 0)
        {
            tasks.levelEnd = true;
            tasks.EndLevel();
        }

        scoreText.text = "score: " + currentScore;
        starText.text = "stars: " + CheckStars();
    }

    public void AddScore(float scoreIn)
    {
        currentScore += scoreIn;

        if (currentScore < 0) currentScore = 0;

        Debug.Log(currentScore);
    }

    private int CheckStars()
    {
        if (currentScore >= starScore3) return 3;
        else if(currentScore >= starScore2) return 2;
        else if (currentScore >= starScore1) return 1;
        else return 0;
    }
}

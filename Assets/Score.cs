using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void SaveScore(int currentScore)
    {
        if(PlayerPrefs.GetInt("Score") < currentScore)
        {
            PlayerPrefs.SetInt("Score", currentScore);
        }
    }
}

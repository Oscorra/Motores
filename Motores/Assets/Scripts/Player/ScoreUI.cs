using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int score = 0;

    private void OnEnable()
    {
        CoinPickup.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        CoinPickup.OnCoinCollected -= UpdateScore;
    }
    void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshPro scoreText;

    int score = 0;

    private void OnEnable()
    {
        OnPickUpCoin.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        OnPickUpCoin.OnCoinCollected -= UpdateScore;
    }
    void UpdateScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score;
    }

}

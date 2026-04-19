using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    void OnEnable() { OnPickUpCoin.OnCoinCollected += AddScore; }
    void OnDisable() { OnPickUpCoin.OnCoinCollected -= AddScore; }
    void AddScore(int points)
    {
        totalScore += points;
        Debug.Log($"Score: {totalScore}");
    }
}

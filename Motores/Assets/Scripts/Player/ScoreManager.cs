using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int totalScore;
    void OnEnable() { CoinPickup.OnCoinCollected += AddScore; }
    void OnDisable() { CoinPickup.OnCoinCollected -= AddScore; }
    void AddScore(int points)
    {
        totalScore += points;
        Debug.Log($"Score: {totalScore}");
    }
}

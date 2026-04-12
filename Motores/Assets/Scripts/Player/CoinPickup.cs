using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public static event Action<int> OnCoinCollected;
    public int pointValue = 10;
    public AudioSource moneda;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player_Movement>() != null)
        {
            OnCoinCollected.Invoke(pointValue);
            Destroy(gameObject);
            moneda.Play();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnPickUpCoin : MonoBehaviour
{
    public event Action<int> OnCoinCollected;

    public int pointValue = 10;
    public AudioSource coinSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinCollected?.Invoke(pointValue);

            if (coinSound != null)
                AudioSource.PlayClipAtPoint(coinSound.clip, transform.position);

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float fov = 60f;
    void Update()
    {
        Vector3 dirEnemy = transform.forward;
        Vector3 dirPlayer = (player.position - transform.position).normalized;

        float dot = Vector3.Dot(dirEnemy, dirPlayer);
        float limit = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);

        if (dot > limit / 2f)
        {
            Debug.Log("Player detected");
        }
    }
}

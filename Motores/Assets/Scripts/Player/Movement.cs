using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator anim;
    private Player_Movement Dinamica;
    void Start()
    {
        anim = GetComponent<Animator>();
        Dinamica = GetComponent<Player_Movement>();
    }


    void Update()
    {
        anim.SetFloat("VelZ", Dinamica.inputZ);
        anim.SetFloat("VelX", Dinamica.inputX);
    }
}

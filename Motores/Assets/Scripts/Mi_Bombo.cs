using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mi_Bombo : MonoBehaviour
{
    public GameObject canvas;
    public AudioSource sonido;
    private bool jugadorDentro = false;

    void Start()
    {
        canvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            canvas.SetActive(false);
        }
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            sonido.Play();
        }
    }
}

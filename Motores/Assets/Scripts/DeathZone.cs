using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Arrastra aquí un objeto que sirva de punto de reaparición (SpawnPoint)
    public Transform puntoDeReinicio;

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si lo que ha entrado es el jugador
        if (other.CompareTag("Player"))
        {
            // Movemos al jugador a la posición del punto de reinicio
            other.transform.position = puntoDeReinicio.position;

            // Si el jugador tiene un Rigidbody, reseteamos su velocidad para que no siga cayendo
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
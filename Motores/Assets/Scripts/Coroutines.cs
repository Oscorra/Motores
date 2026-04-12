using UnityEngine;
using System.Collections; 

public class Corotines : MonoBehaviour
{
    public Vector3 direccion = new Vector3(5, 0, 0); // Distancia a recorrer
    public float tiempoMovimiento = 2f;             // Cuánto tarda en ir
    public float tiempoEspera = 1f;                 // Pausa en los extremos

    private Vector3 posicionInicio;
    private Vector3 posicionFin;

    void Start()
    {
        posicionInicio = transform.position;
        posicionFin = posicionInicio + direccion;

        StartCoroutine(BuclePlataforma());
    }

    IEnumerator BuclePlataforma()
    {
        while (true) 
        {
            
            yield return StartCoroutine(MoverObjeto(posicionInicio, posicionFin));

            yield return new WaitForSeconds(tiempoEspera);

            yield return StartCoroutine(MoverObjeto(posicionFin, posicionInicio));

            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    IEnumerator MoverObjeto(Vector3 origen, Vector3 destino)
    {
        float paso = 0f;
        while (paso < 1f)
        {
            paso += Time.deltaTime / tiempoMovimiento;
            transform.position = Vector3.Lerp(origen, destino, paso);

            // Pausa la ejecución por un frame (resume next Update)
            yield return null; 
        }
    }

    // Para que el jugador se mueva con la plataforma, hacemos que sea hijo de ella mientras esté encima
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
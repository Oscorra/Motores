using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private MeshRenderer m_Renderer_Jugador;
    private Rigidbody rb_Jugador;

    public float multiplicadorDesplazamiento = 8.0f;
    public Transform cameraTransform;

    private Animator move;

    public float inputX { get; private set; }
    public float inputZ { get; private set; }
    private Vector3 direccionMovimiento;

    [Header("Salto")]
    public float fuerzaSalto = 6.0f;
    private bool estaEnSuelo;
    private bool saltoPendiente;

    void Start()
    {
        m_Renderer_Jugador = GetComponent<MeshRenderer>();
        rb_Jugador = GetComponent<Rigidbody>();
        move = GetComponent<Animator>();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        direccionMovimiento = (camForward * inputZ + camRight * inputX);

        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            saltoPendiente = true;
        }
    }

    void FixedUpdate()
    {
        if (direccionMovimiento.sqrMagnitude > 0.1f)
        {
            direccionMovimiento.Normalize();
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, 0.2f);

            Vector3 direccion = rb_Jugador.position + direccionMovimiento * multiplicadorDesplazamiento * Time.fixedDeltaTime;
            rb_Jugador.MovePosition(direccion);
        }

        // Aplicar salto
        if (saltoPendiente)
        {
            rb_Jugador.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            saltoPendiente = false;
        }

        Vector3 movimientoLocal = transform.InverseTransformDirection(direccionMovimiento);

        if (move != null)
        {
            move.SetFloat("VelZ", movimientoLocal.z);
            move.SetFloat("VelX", movimientoLocal.x);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        estaEnSuelo = true;
    }

    void OnCollisionExit(Collision collision)
    {
        estaEnSuelo = false;
    }
}

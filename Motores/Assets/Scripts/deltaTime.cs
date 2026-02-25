using UnityEngine;
using System.Collections;

public class deltaTime : MonoBehaviour
{
    public float distancePerSecond = 5f;
    public float distancePerFrame = 0.02f;

    public float rotationSpeed = 90f;
    public bool useDeltaTimeMovement = true;
    public bool rotationEnabled = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            useDeltaTimeMovement = !useDeltaTimeMovement;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotationEnabled = !rotationEnabled;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            distancePerSecond += 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            distancePerSecond = distancePerSecond - 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotationSpeed += 15f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotationSpeed -= 15f;
        }

        //Reset
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

        // MOVIMIENTO
        if (useDeltaTimeMovement)
        {
            // independiente de fps
            transform.Translate(0, 0, distancePerSecond * Time.deltaTime);
        }
        else
        {
            // depende de fps
            transform.Translate(0, 0, distancePerFrame);
        }
    }

    void FixedUpdate()
    {
        // ROTACION
        if (rotationEnabled)
        {
            transform.Rotate(0, rotationSpeed * Time.fixedDeltaTime, 0);
        }
    }
void OnGUI()
{
    // Estilos 
    GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
    boxStyle.fontSize = 18;
    boxStyle.alignment = TextAnchor.UpperLeft;
    boxStyle.padding = new RectOffset(12, 12, 12, 12);

    GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
    labelStyle.fontSize = 20;   // tamaño del texto
    labelStyle.normal.textColor = Color.white;

    GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
    titleStyle.fontSize = 22;
    titleStyle.fontStyle = FontStyle.Bold;
    titleStyle.normal.textColor = Color.white;

    //GUI.Box(new Rect(10, 10, 560, 250), "");

    // Título


    // Controles
    GUI.Label(new Rect(25, 60, 660, 28), "SPACE -> Cambiar movimiento (con/sin deltaTime)", labelStyle);
    GUI.Label(new Rect(25, 90, 660, 28), "R -> Activar/Desactivar rotación (FixedUpdate)", labelStyle);
    GUI.Label(new Rect(25, 120, 660, 28), "UP / DOWN -> Subir/Bajar distancePerSecond", labelStyle);
    GUI.Label(new Rect(25, 150, 660, 28), "RIGHT / LEFT -> Subir/Bajar rotationSpeed", labelStyle);
    GUI.Label(new Rect(25, 180, 660, 28), "T -> Reset posición/rotación", labelStyle);
    

    // Estado actual
    GUI.Label(
        new Rect(25, 250, 660, 28),
        "Movimiento: " + (useDeltaTimeMovement ? "deltaTime (correcto)" : "perFrame (incorrecto)"),
        labelStyle
    );

    GUI.Label(
        new Rect(25, 280, 660, 28),
        "distancePerSecond: " + distancePerSecond.ToString("F2") +
        " | rotationSpeed: " + rotationSpeed.ToString("F2"),
        labelStyle
    );

    GUI.Label(
        new Rect(25, 310, 660, 28),
        "deltaTime: " + Time.deltaTime.ToString("F4") +
        " | fixedDeltaTime: " + Time.fixedDeltaTime.ToString("F4"),
        labelStyle
    );
}
}


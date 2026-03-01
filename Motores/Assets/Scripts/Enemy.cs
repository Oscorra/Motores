using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("FOV Settings")]
    public Transform player;
    public float fov = 60f;
    public float detectionRange = 10f;


    [Header("Visual Settings")]
    public Color fovColor = new Color(1f, 0f, 0f, 0.3f);
    public int fovResolution = 30;
    public bool showFOVInGame = true;

    private bool playerDetected = false;

    private Mesh fovMesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Crear el mesh visual del FOV
        CreateFOVVisualization();
    }

    void Update()
    {
        // Detección del jugador
        DetectPlayer();

        if (showFOVInGame)
        {
            DrawFOV();
        }
    }

    void DetectPlayer()
    {
        if (player == null)
        {
            playerDetected = false;
            return;
        }

        Vector3 dirToPlayer = player.position - transform.position;
        float distanceToPlayer = dirToPlayer.magnitude;

        // Verificar si está dentro del rango
        if (distanceToPlayer > detectionRange)
        {
            playerDetected = false;
            return;
        }

        // Calcular el ángulo
        Vector3 dirEnemy = transform.forward;
        float dot = Vector3.Dot(dirEnemy, dirToPlayer.normalized);
        float limit = Mathf.Cos(fov * 0.5f * Mathf.Deg2Rad);

        if (dot > limit)
        {
            playerDetected = true;
            Debug.Log("Player detected");
        }else
        {
            playerDetected = false;
        }
    }


    //visualización del FOV

    void CreateFOVVisualization()
    {
        // Crear un GameObject hijo para el mesh del FOV
        GameObject fovObject = new GameObject("FOV Visualization");
        fovObject.transform.parent = transform;
        fovObject.transform.localPosition = Vector3.zero;
        fovObject.transform.localRotation = Quaternion.identity;

        meshFilter = fovObject.AddComponent<MeshFilter>();
        meshRenderer = fovObject.AddComponent<MeshRenderer>();

        fovMesh = new Mesh();
        fovMesh.name = "FOV Mesh";
        meshFilter.mesh = fovMesh;

        // Material para el FOV
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        material.color = fovColor;
        meshRenderer.material = material;

        // Desactivar sombras
        meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        meshRenderer.receiveShadows = false;
    }

    void DrawFOV()
    {
        int segments = fovResolution;
        float halfFOV = fov * 0.5f;
        float angleStep = fov / segments;

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; // Centro del FOV

        for (int i = 0; i <= segments; i++)
        {
            float angle = -halfFOV + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            vertices[i + 1] = direction * detectionRange;
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        fovMesh.Clear();
        fovMesh.vertices = vertices;
        fovMesh.triangles = triangles;
        fovMesh.RecalculateNormals();
    }

    // Dibujar el FOV en el editor (Scene view)
    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Dibujar las líneas del FOV
        Vector3 leftBoundary = Quaternion.Euler(0, -fov * 0.5f, 0) * transform.forward * detectionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, fov * 0.5f, 0) * transform.forward * detectionRange;

        Gizmos.color = playerDetected ? Color.red : Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);

        // Dibujar línea hacia el jugador si está detectado
        if (playerDetected)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    // GUI para el warning
    void OnGUI()
    {
        if (playerDetected)
        {
            // Estilo para el warning
            GUIStyle warningStyle = new GUIStyle(GUI.skin.box);
            warningStyle.fontSize = 30;
            warningStyle.fontStyle = FontStyle.Bold;
            warningStyle.normal.textColor = Color.red;
            warningStyle.alignment = TextAnchor.MiddleCenter;

            // Calcular posición en pantalla
            float warningWidth = 300;
            float warningHeight = 80;
            float warningX = (Screen.width - warningWidth) / 2;
            float warningY = 50;

            // Dibujar el warning
            GUI.Box(new Rect(warningX, warningY, warningWidth, warningHeight), "⚠ WARNING ⚠", warningStyle);

            // Texto adicional
            GUIStyle textStyle = new GUIStyle(GUI.skin.label);
            textStyle.fontSize = 20;
            textStyle.normal.textColor = Color.yellow;
            textStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(warningX, warningY + 60, warningWidth, 30), "ENEMY DETECTED YOU!", textStyle);
        }
    }
}

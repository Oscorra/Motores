using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Switch : MonoBehaviour
{
    public Transform playerView;
    public Transform topDownView;
    public float transitionSpeed = 2f;

    public Camera_Rotation rotationScript;

    bool toTopDown = false;
    bool isTransitioning = false;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Cambiar cámara
        if (Input.GetKeyDown(KeyCode.C))
        {
            toTopDown = !toTopDown;
            isTransitioning = true;

            rotationScript.enabled = !toTopDown;
        }

        // Transición suave
        if (isTransitioning)
        {
            Transform target = toTopDown ? topDownView : playerView;

            transform.position = Vector3.Lerp(
                transform.position,
                target.position,
                Time.deltaTime * transitionSpeed
            );

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                target.rotation,
                Time.deltaTime * transitionSpeed
            );

            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
                isTransitioning = false;
            }
        }
    }
}

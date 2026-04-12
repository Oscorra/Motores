using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Camera playerCamera;
    public Camera topDownCamera;

    private Camera activeCamera;
    private bool topDownActive = false;

    void Start()
    {
        playerCamera.gameObject.SetActive(true);
        topDownCamera.gameObject.SetActive(false);

        activeCamera = playerCamera;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            topDownActive = !topDownActive;

            playerCamera.gameObject.SetActive(!topDownActive);
            topDownCamera.gameObject.SetActive(topDownActive);

            activeCamera = topDownActive ? topDownCamera : playerCamera;

            if (topDownActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            DoRaycast();
        }
    }

    void DoRaycast()
    {
        Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red, 0.2f);

        if (Physics.Raycast(ray, out RaycastHit hit, 200f))
        {
            Debug.Log("Has clicado: " + hit.collider.name);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTorque : MonoBehaviour
{
    public void AddRelativeTorque(Vector3 torque, ForceMode mode = ForceMode.Force)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddRelativeTorque(torque, mode);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddRelativeTorque(Vector3.up * 900f, ForceMode.Impulse);
        }
    }
}
  
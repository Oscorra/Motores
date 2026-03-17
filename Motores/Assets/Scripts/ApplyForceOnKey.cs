using UnityEngine;

public class ApplyForceOnKey : MonoBehaviour
{
    public Rigidbody rb;
    public float forceAmount = 30f;
    public Vector3 direction = Vector3.forward;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(direction.normalized * forceAmount);
        }
    }
}

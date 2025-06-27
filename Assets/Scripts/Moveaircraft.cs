using UnityEngine;

public class MoveAircraft : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 20f;
    public float rotationSpeed = 100f;
    public float maxSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Quaternion deltaRotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 forceDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 force = forceDirection * moveInput * acceleration;

        rb.AddForce(force);

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(horizontalVelocity.x, 0f, horizontalVelocity.z);
        }

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    }
}

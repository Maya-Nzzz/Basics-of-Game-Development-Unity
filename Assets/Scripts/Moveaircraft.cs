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

        // Фиксируем движение по Y и вращения, кроме по Y (вращение влево/вправо)
        rb.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");     // Вперёд / назад
        float turnInput = Input.GetAxis("Horizontal");   // Поворот влево / вправо

        // Поворот вокруг Y
        Quaternion deltaRotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Применение силы вперёд по направлению объекта (только XZ-плоскость)
        Vector3 forceDirection = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 force = forceDirection * moveInput * acceleration;

        rb.AddForce(force);

        // Ограничение максимальной скорости только по XZ
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(horizontalVelocity.x, 0f, horizontalVelocity.z);
        }

        // Жестко сбрасываем любое движение по Y
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
    }
}

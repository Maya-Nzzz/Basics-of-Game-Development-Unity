using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float acceleration = 20f;
    public float maxSpeed = 6f;
    public float drag = 5f;

    [Header("Rotation")]
    public float mouseSensitivity = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.forward * moveVertical + transform.right * moveHorizontal;
        moveDirection.Normalize();

        if (moveDirection.magnitude > 0.1f)
        {
            rb.AddForce(moveDirection * acceleration, ForceMode.Acceleration);
        }

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVel = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        // Добавляем сопротивление (drag)
        rb.linearVelocity = new Vector3(
            Mathf.Lerp(rb.linearVelocity.x, 0, drag * Time.fixedDeltaTime),
            rb.linearVelocity.y,
            Mathf.Lerp(rb.linearVelocity.z, 0, drag * Time.fixedDeltaTime)
        );
    }
}

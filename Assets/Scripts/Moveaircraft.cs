using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveaircraft : MonoBehaviour
{
    private bool canJump = true;
    public float jumpCooldown = 2.0f;

    public float speed = 3.0f;
    public float maxSpeed = 6.0f;
    public float rotationSpeed = 360.0f;
    public float jumpForce = 5.0f;
    public float hoverHeight = 2.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float rotationY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationY, 0);

        Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
        rb.AddForce(movement.normalized * speed, ForceMode.Acceleration);

        Vector3 clampedVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        rb.linearVelocity = clampedVelocity;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            StartCoroutine(EnableJump());
        }

        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit, hoverHeight))
        {
            float distanceToGround = hit.distance;
            float adjustment = Mathf.Clamp((hoverHeight - distanceToGround) * 10f, 0, jumpForce);
            rb.AddForce(Vector3.up * adjustment, ForceMode.Acceleration);
        }
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        canJump = true;
    }
}

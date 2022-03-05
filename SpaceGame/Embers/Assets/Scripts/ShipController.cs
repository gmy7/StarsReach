using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 0.5f;
    public float acceleration = 0.25f;
    public float rotationSpeed = 0.25f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 force = transform.up;

        float clampedInput = Mathf.Clamp(verticalInput, 0, 1); 
        rb.AddForce(force * clampedInput);

        transform.Rotate(0f, 0f, -horizontalInput * rotationSpeed);

        if(Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            Vector2 normalizedVelocity = rb.velocity;
            normalizedVelocity.Normalize();
            rb.velocity = normalizedVelocity * maxSpeed;
        }
    }
}

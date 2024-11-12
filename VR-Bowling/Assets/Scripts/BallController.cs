using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    private Rigidbody rb;

    public bool ballThrown = false;

    private Vector3 ballStartPosition; // Starting position of the ball for resetting

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Store the starting position of the ball for resetting
        ballStartPosition = transform.position;
    }

    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        float moveForward = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Apply movement to the ball
        Vector3 movement = new Vector3(-moveRight, 0f, moveForward);
        rb.AddForce(movement, ForceMode.VelocityChange);

        //TODO: Check if the ball has been "thrown" or moved
        if (movement.magnitude > 0.1f)
        {
            ballThrown = true;
        }
    }

    // Reset the ball to its original position and stop its motion
    public void ResetBall()
    {
        transform.position = ballStartPosition;  // Teleport the ball back to its starting position
        rb.velocity = Vector3.zero;              // Stop all movement
        rb.angularVelocity = Vector3.zero;       // Stop all rotation
        ballThrown = false;                      // Reset the ball thrown state
    }

    public bool BallThrown
    {
        get { return ballThrown; }
    }
}
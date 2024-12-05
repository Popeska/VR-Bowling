using UnityEngine;

public class KeyboardBallController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Apply movement to the ball
        Vector3 movement = new Vector3(moveRight, 0f, moveForward);
        rb.AddForce(movement, ForceMode.VelocityChange);

        // Optional: Rotate the ball with Q/E or left/right arrow keys
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
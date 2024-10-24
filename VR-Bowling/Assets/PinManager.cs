using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject[] pins; // Array to hold the pin GameObjects
    private int score = 0; // Current score
    private Vector3[] startingPositions; // Array to store the starting positions of the pins
    private int totalPins; // Total number of pins

    public GameObject ball; // Bowling ball object
    private Vector3 ballStartingPosition;   // Starting position of bowling ball

    void Start()
    {
        totalPins = pins.Length; // Initialize the total number of pins
        startingPositions = new Vector3[totalPins]; // Initialize the array to store starting positions

        // Store the starting positions of each pin
        for (int i = 0; i < totalPins; i++)
        {
            startingPositions[i] = pins[i].transform.position; // Store each pin's position
        }
        ballStartingPosition = ball.transform.position;
        score = 0;
    }

    void Update()
    {
        // Check if the frame is over and pins need to be reset
        if (Input.GetKeyDown(KeyCode.R)) // Use 'R' key to reset for testing
        {
            ResetPins();
        }

        // Check for knocked down pins
        CheckPinsStatus();
    }

    void CheckPinsStatus()
    {
        int knockedDownCount = 0;

        foreach (GameObject pin in pins)
        {
            // Assuming the pin has a Rigidbody component
            Rigidbody rb = pin.GetComponent<Rigidbody>();
            if (rb != null && (rb.rotation.eulerAngles.x < 255f || rb.rotation.eulerAngles.x > 285f)) // Check if pin is knocked down
            {
                knockedDownCount++;
                Debug.Log(rb.rotation.eulerAngles.x);
            }
        }

        // Update score based on knocked down pins
        score = knockedDownCount; // Simple score for knocked down pins
        Debug.Log("Score: " + score);
    }

    void ResetPins()
    {
        // Reset score
        score = 0;

        for (int i = 0; i < totalPins; i++)
        {
            // Teleport pin back to its starting position
            pins[i].transform.position = startingPositions[i];

            // Reset rotation
            pins[i].transform.rotation = Quaternion.Euler(270, 0, 0);

            // Optionally reset the Rigidbody if needed
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop any motion
                rb.angularVelocity = Vector3.zero; // Stop any rotation
            }
        }
        // Do the same but for the bowling ball
        ball.transform.position = ballStartingPosition;

        Rigidbody ballrb = ball.GetComponent<Rigidbody>();
        if (ballrb != null)
            {
                ballrb.velocity = Vector3.zero; // Stop any motion
                ballrb.angularVelocity = Vector3.zero; // Stop any rotation
            }
    }
}
// Kelly- testing Git push
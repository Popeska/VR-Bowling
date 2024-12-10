using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Reference BallReturner Script
    private BallReturner ballReturner;

    // Get from ballReturner
    private Transform spawnPoint;
    void Start()
    {
        // Get script
        ballReturner = FindObjectOfType<BallReturner>();

        // Get initial spawnPoint for ball pos reset
        spawnPoint = ballReturner.spawnPoints[ballReturner.ballsOnReturner.IndexOf(gameObject)];

    }

    public void ReturnToSpawn() {
        if (spawnPoint != null)
        {
            // Return ball to the spawn point
            transform.position = spawnPoint.position;
            gameObject.SetActive(false); // Optionally hide the ball
            gameObject.SetActive(true);  // Reactivate it if needed
        }
    }
    // Trigger detection for when ball falls through lane
    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball falls through the lane (you can use a tag or layer)
        if (other.CompareTag("BallReturnZone")) // Assuming you have a tag or layer for the return zone
        {
            ReturnToSpawn();
        }
    }
}

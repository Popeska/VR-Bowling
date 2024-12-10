using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    // Reference BallReturner Script
    private BallReturner ballReturner;

    // Get from ballReturner
    private Vector3 spawnPoint;

    private bool instancedBall;
    void Start()
    {
        // Get script
        ballReturner = FindObjectOfType<BallReturner>();

        // Get initial spawnPoint for ball pos reset
        if (instancedBall == true)
        {
            spawnPoint = ballReturner.spawnPoints[ballReturner.ballsOnReturner.IndexOf(gameObject)];
        }
        else 
        {
            spawnPoint = transform.position;
        }

    }

    public void ReturnToSpawn() {
        if (spawnPoint != null)
        {
            // Return ball to the spawn point
            transform.position = spawnPoint;
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

    public void SetInstancedBall(bool ans)
    {
        instancedBall = ans;
    }
}

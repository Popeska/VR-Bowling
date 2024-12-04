using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public GameObject[] pins; // Array of pin GameObjects
    private int totalPins; // Total number of pins
    private int knockedDownPins; // Count of knocked down pins
    private Vector3[] startingPositions;

    void Start()
    {
        totalPins = pins.Length; // Initialize total pins
        startingPositions = new Vector3[totalPins]; // Initialize the array to store starting positions

        // Store the starting positions of each pin
        for (int i = 0; i < totalPins; i++)
        {
            startingPositions[i] = pins[i].transform.position; // Store each pin's position
        }
        ResetPins(); // Reset pins at the start
    }

    void Update()
    {
        // You can add logic here if you want to check pin states continuously
        if(Input.GetKeyDown(KeyCode.Space)){
            //ResetPins();
            int pinsknock = GetKnockedDownCount();
            Debug.Log("PM: " + pinsknock);
       }
       
    }

    // Method to reset all pins to their original position
    public void ResetPins()
    {
        for (int i = 0; i < totalPins; i++)
        {
            // Teleport pin back to its starting position
            pins[i].transform.position = startingPositions[i];

            // Reset rotation
            pins[i].transform.rotation = Quaternion.Euler(0, 0, 0);

            // Optionally reset the Rigidbody if needed
            Rigidbody rb = pins[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop any motion
                rb.angularVelocity = Vector3.zero; // Stop any rotation
            }
        }
        knockedDownPins = 0; // Reset knocked down pins count
    }

    // Method to check how many pins have been knocked down
    public int GetKnockedDownCount()
    {
        knockedDownPins = 0; // Reset count for this check
        foreach (GameObject pin in pins)
        {
            Rigidbody rb = pin.GetComponent<Rigidbody>();
            if (rb != null && (rb.rotation.eulerAngles.z < -15f || rb.rotation.eulerAngles.z > 15f)) // Check if pin is knocked down
            {
                knockedDownPins++;
            }
        }
        return knockedDownPins; // Return the count of knocked down pins
    }

    // Method to check if the round is over (all pins knocked down)
    public bool CheckIfRoundOver()
    {
        return knockedDownPins >= totalPins; // Return true if all pins are knocked down
    }
}
/* 
 * This script instantiates a set number of bowling balls with randomized UV coordinates.
 * Also handles when ball "drops" from end of lane and returns it to the ball returner.
 */
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class BallReturner : MonoBehaviour
{
    public List<Transform> spawnPoints; // List of coordinates where ball is instanced/returned
    
    public GameObject ballPrefab;
    public List<GameObject> ballsOnReturner; // The # of balls on returner
    private int spawnLimit = 3; // Max # of balls can spawn

    public List<float> uvOffset; // Offset Y for ball texture

    private Ball[] sceneBalls; // Balls already instanced in scene




    // Start is called before the first frame update
    void Start()
    {
        // 0 - 3
        uvOffset.Add(0.0f);
        uvOffset.Add(0.2482f);
        uvOffset.Add(0.2482f*2);
        uvOffset.Add(0.2482f*3);

        sceneBalls = FindObjectsOfType<Ball>();

    }

    
    // Instantiates and Randomizes ball texture
    private void RandomizeUV(GameObject ball)
    {

        Renderer ballRenderer = ball.GetComponent<Renderer>();

        if (ballRenderer != null)
        {
            Material ballMaterial = ballRenderer.material;

            // Generate a random UV offset

            float randomOffsetY = Random.Range(0, uvOffset.Count);

            // Now apply it to the ball texture
            // Apply the random offset to the material's main texture
            ballMaterial.SetTextureOffset("_MainTex", new Vector2(0f, randomOffsetY));

            // If you have additional maps, apply the same offset
            // ballMaterial.SetTextureOffset("_BumpMap", new Vector2(randomOffsetX, randomOffsetY));

        }

    }

    // Randomize UVs for balls already in scene
    public void RandomizeUVCurrentBalls() 
    {
        foreach (Ball ball in sceneBalls)
        {
            Debug.Log("Ball found: " + ball.gameObject.name);
        }
    }


    // Function to create a new ball
    // This is the FIRST set of balls
    public void CreateNewBall()
    {
        // 
        for (int i = 0; i < spawnLimit; i++) {
            // Instantiate a new ball at the spawn point
            GameObject newBall = Instantiate(ballPrefab, spawnPoints[i].position, Quaternion.identity);
            
            // Randomize its Y UV texture offset
            RandomizeUV(newBall);

            // Add the new ball to the returner queue
            // MAYBE delete
            ballsOnReturner.Add(newBall);

        }        
        
    }
}

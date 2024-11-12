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
    public Transform spawnPoint; // Where ball is instanced/returned. Might change depending on lane played.
    
    public GameObject ballPrefab;
    public List<GameObject> ballsOnReturner; // The # of balls on returner
    public int spawnLimit; // Max # of balls can spawn

    public List<float> uvOffset; // Offset Y for ball texture

    // For the balls to roll forawrd
    public float moveSpeed = 2f;
    public float rotateSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // 0 - 3
        uvOffset.Add(0.0f);
        uvOffset.Add(0.2482f);
        uvOffset.Add(0.2482f*2);
        uvOffset.Add(0.2482f*3);
    }

    // Call this to return the ball when it drops end of lane
    public void ReturnBall()
    {

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

    // Coroutine to move and rotate the balls forward
    //private IEnumerator MoveAndRotateBallsForward()
    

    // Trigger detection for when ball fall through lane
    private void OnTriggerEnter(Collider other)
    {
        
    }

    // Function to create a new ball
    public void CreateNewBall()
    {
        // Instantiate a new ball at the spawn point
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        // Randomize its Y UV texture offset
        RandomizeUV(newBall);

        // Add the new ball to the returner queue
        ballsOnReturner.Add(newBall);

        // Start moving and rotating the balls forward
        //StartCoroutine(MoveAndRotateBallsForward());
    }
}

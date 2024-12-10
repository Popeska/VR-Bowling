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

    public List<float> uvOffset; // Offset Y for ball texture

    private Renderer ballRenderer;
    private Material ballMaterial;
    private LODGroup lodGroup;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ballRenderer = GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            // Create a unique material instance for the ball if necessary
            ballMaterial = ballRenderer.material;
        }

        Debug.Log(ballMaterial);

        // Store the starting position of the ball for resetting
        ballStartPosition = transform.position;

        // 0 - 3
        uvOffset.Add(0.0f);
        uvOffset.Add(0.2482f);
        uvOffset.Add(0.2482f*2);
        uvOffset.Add(0.2482f*3);

        RandomizeUV();
        // If LODGroup exists, we apply texture offset to all LOD materials
        lodGroup = GetComponent<LODGroup>();
        if (lodGroup != null)
        {
            ApplyTextureOffsetToAllLODs();
        }
    }

    void Update()
    {
        
       if(Input.GetKeyDown(KeyCode.Space)){
            ResetBall();
       }

        //TODO: Check if the ball has been "thrown" or moved
        if (rb.velocity.magnitude > 1f)
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

    public bool ballStopped(){
        return rb.velocity.magnitude < 0.1f;
    }

    // Randomizes ball texture
    private void RandomizeUV()
    {
        if (ballRenderer != null && ballMaterial != null)
        {
            // Pick random uv coord y
            int randomIndex = Random.Range(0, uvOffset.Count);
            float randomOffsetY = uvOffset[randomIndex];

            // Log for debugging
            Debug.Log($"Applying texture offset: {randomOffsetY}");

            // Set the texture offset for the albedo texture (_BaseMap)
            int baseMapID = Shader.PropertyToID("_BaseMap");
            if (ballMaterial.HasProperty(baseMapID))
            {
                ballMaterial.SetTextureOffset(baseMapID, new Vector2(0f, randomOffsetY));
                Debug.Log($"Texture offset applied: {ballMaterial.GetTextureOffset(baseMapID)}");
            }
            else
            {
                Debug.LogWarning("BaseMap not found on the material!");
            }
        }
        else
        {
            Debug.LogWarning("Renderer or Material not found!");
        }
    }

    // Apply texture offset to all materials used in LODs
    private void ApplyTextureOffsetToAllLODs()
    {
        LOD[] lods = lodGroup.GetLODs();
        foreach (var lod in lods)
        {
            foreach (Renderer lodRenderer in lod.renderers)
            {
                if (lodRenderer != null && lodRenderer.material != null)
                {
                    Material material = lodRenderer.material;
                    ApplyTextureOffset(material);
                }
            }
        }
    }

    // Helper method to apply the texture offset to a given material
    private void ApplyTextureOffset(Material material)
    {
        int baseMapID = Shader.PropertyToID("_BaseMap");
        if (material.HasProperty(baseMapID))
        {
            material.SetTextureOffset(baseMapID, new Vector2(0f, uvOffset[Random.Range(0, uvOffset.Count)]));
            Debug.Log($"Texture offset applied to material: {material.name}");
        }
        else
        {
            Debug.LogWarning($"BaseMap not found on material: {material.name}");
        }
    }
}
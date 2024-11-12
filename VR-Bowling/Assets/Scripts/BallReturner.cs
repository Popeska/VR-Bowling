/* 
 * This script instantiates a set number of bowling balls with randomized UV coordinates.
 * Also handles when ball "drops" from end of lane and returns it to the ball returner.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReturner : MonoBehaviour
{
    public Transform spawnPoint; // Where ball is instanced/returned. Might change depending on lane played.
    
    public GameObject ballPrefab;
    public List<GameObject> ballsOnReturner; // The # of balls on returner
    public int spawnLimit; // Max # of balls can spawn

    public List<Vector2> uvOffset; // List of offset for ball texture


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

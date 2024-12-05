using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PinManager pinManager; // Manages pin states
    public BallController ballController; // Handles ball control
    public ScoreKeeper scoreKeeper; // Tracks and displays scores

    private const int maxFrames = 10; // Max number of frames in a game
    private bool frameInProgress = false; // Is the current frame active
    bool rollHandled = true;
    bool gameOver = false;

    void Start()
    {
        StartFrame(); // Begin the first frame
    }

    void Update()
    {
        if(gameOver){
            return;
        }
        // Check if the ball has reached the -8 Z position and has been thrown
        if (rollHandled && ballController.transform.position.y <= -10f && scoreKeeper.getCurrentFrame() <= 10)
        {
            // Handle the roll or next step in the game loop
            rollHandled = false;
            Debug.Log("We are handling a roll");
            HandleRoll();

        }
        if(scoreKeeper.getCurrentFrame() > 10){
            EndGame();
        }
    }

    void StartFrame()
    {
        Debug.Log($"Starting Frame {scoreKeeper.getCurrentFrame()}");
        pinManager.ResetPins(); // Reset pins
        ballController.ResetBall(); // Reset ball
        frameInProgress = true;
    }

    void HandleRoll()
    {
        int pinsKnocked = pinManager.GetKnockedDownCount();
        scoreKeeper.RecordRoll(pinsKnocked);
        Debug.Log("GM: " + pinsKnocked);

        if (scoreKeeper.getCurrentRoll() == 1)
        {
            if (pinsKnocked == 10) // Strike
            {
                ballController.ResetBall();
                Debug.Log("Strike!");
                EndFrame();
            }
            else
            {
                ballController.ResetBall(); // Reset ball for next roll
            }
        }
        else if (scoreKeeper.getCurrentRoll() == 2)
        {
            Debug.Log($"End of Frame {scoreKeeper.getCurrentFrame()}");
            EndFrame();
        }
        rollHandled = true;

    }

    void EndFrame()
    {
        frameInProgress = false;
        int currentFrame = scoreKeeper.getCurrentFrame();

        if (currentFrame < maxFrames)
        {
            StartFrame(); // Start the next frame
        }
        else
        {
            EndGame(); // End the game after the final frame
        }
    }

    bool BallStopped()
    {
        return ballController.GetComponent<Rigidbody>().velocity.magnitude < 0.1f; // Check if the ball has stopped
    }

    void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + scoreKeeper.GetTotalScore());
        gameOver = true;
        // TODO: Display final score UI and restart option
    }
}
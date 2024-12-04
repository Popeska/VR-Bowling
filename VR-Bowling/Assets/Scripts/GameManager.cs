using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PinManager pinManager; // Manages pin states
    public BallController ballController; // Handles ball control
    public ScoreKeeper scoreKeeper; // Tracks and displays scores

    private int currentFrame = 1; // Track the current frame (1-10)
    private int currentRoll = 1; // Track the roll within the frame (1 or 2)
    private const int maxFrames = 10; // Max number of frames in a game
    private bool frameInProgress = false; // Is the current frame active

    void Start()
    {
        StartFrame(); // Begin the first frame
    }

    void Update()
    {
        if (frameInProgress && ballController.BallThrown)
        {
            if (pinManager.CheckIfRoundOver() || BallStopped())
            {
                HandleRoll();
            }
        }
    }

    void StartFrame()
    {
        Debug.Log($"Starting Frame {currentFrame}");
        pinManager.ResetPins(); // Reset pins
        ballController.ResetBall(); // Reset ball
        currentRoll = 1;
        frameInProgress = true;
    }

    void HandleRoll()
    {
        int pinsKnocked = pinManager.GetKnockedDownCount();
        scoreKeeper.RecordRoll(pinsKnocked);

        if (currentRoll == 1)
        {
            if (pinsKnocked == 10) // Strike
            {
                Debug.Log("Strike!");
                EndFrame();
            }
            else
            {
                currentRoll = 2; // Move to second roll
                ballController.ResetBall(); // Reset ball for next roll
            }
        }
        else if (currentRoll == 2)
        {
            Debug.Log($"End of Frame {currentFrame}");
            EndFrame();
        }
    }

    void EndFrame()
    {
        frameInProgress = false;

        if (currentFrame < maxFrames)
        {
            currentFrame++;
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
        // TODO: Display final score UI and restart option
    }
}
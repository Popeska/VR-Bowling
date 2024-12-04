using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PinManager pinManager; // Reference to PinManager
    public BallController ballController; // Reference to BallController

    public ScoreKeeper scoreKeeper; // Reference to ScoreKeeper
    private int currentFrame = 1;
    private int currentThrow = 1;
    private const int maxFrames = 10;
    private bool frameOver = false; // Track if the current frame is over

    void Update()
    {
        // Check if the ball has been thrown and the frame is over
        if (ballController.BallThrown && !frameOver)
        {
            // Check if all pins are down or if the ball stopped
            if (pinManager.CheckIfRoundOver())
            {
                frameOver = true; // Mark frame as over
                HandleThrow();
            }
        }
    }

    void HandleThrow()
    {
        if (currentThrow == 1)
        {
            if (pinManager.CheckIfRoundOver()) // Strike condition
            {
                Debug.Log("Strike!");
                NextFrame();
            }
            else
            {
                currentThrow = 2; // Move to second throw
                ResetBallOnly();
            }
        }
        else if (currentThrow == 2)
        {
            Debug.Log("End of Frame " + currentFrame);
            NextFrame();
        }
    }

    void NextFrame()
    {
        if (currentFrame < maxFrames)
        {
            currentFrame++;
            currentThrow = 1;
            pinManager.ResetPins(); // Reset all pins
            ballController.ResetBall(); // Reset the ball
        }
        else
        {
            EndGame();
        }

        frameOver = false; // Reset frame status
    }

    void ResetBallOnly()
    {
        ballController.ResetBall(); // Reset ball without resetting pins
        frameOver = false; // Allow next throw
    }

    void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + 40);
        // TODO: Implement end game logic (e.g., show score UI, restart, etc.)
    }

/*
    void ResetGame()
    {
        currentFrame = 1;
        currentThrow = 1;
        totalScore = 0;
        scoreKeeper.ResetScore();
        pinManager.ResetPins();
        ballController.ResetBall();
        UpdateUI();
    }
    */
}

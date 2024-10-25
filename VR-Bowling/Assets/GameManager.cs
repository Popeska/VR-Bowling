using UnityEngine;
using UnityEngine.UI; // Include this for UI elements
using TMPro;

public class GameManager : MonoBehaviour
{
    public PinManager pinManager; // Reference to your PinManager script
    public BallController ballController; // Reference to your BallController script
    public TextMeshProUGUI scoreText; // Reference to your Score Text
    public TextMeshProUGUI frameText; // Reference to your Frame Text
    public TextMeshProUGUI throwText; // Reference to your Throw Text

    private int currentFrame = 1;
    private int currentThrow = 1;
    private const int maxFrames = 10;
    private int totalScore = 0; // Variable to keep track of total score

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        // Check if the ball has been thrown and the round is over
        if (ballController.BallThrown && pinManager.CheckIfRoundOver())
        {
            ProcessThrow();
        }

        // For testing, you can also trigger it manually
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProcessThrow();
        }
    }

    void ProcessThrow()
    {
        // Get the number of knocked down pins
        int knockedDownCount = pinManager.GetKnockedDownCount();
        totalScore += knockedDownCount; // Update total score
        UpdateUI(); // Update the UI with the new score

        // Move to the next throw or frame
        if (currentThrow == 1)
        {
            currentThrow = 2; // Move to the second throw
        }
        else
        {
            // If the second throw is completed, reset for the next frame
            if (currentFrame < maxFrames)
            {
                currentFrame++;
                currentThrow = 1; // Reset to first throw for the next frame
            }
            else
            {
                EndGame(); // End game if maximum frames reached
                return;
            }
        }

        // Reset the ball and pins for the next throw
        pinManager.ResetPins();
        ballController.ResetBall();

        UpdateUI(); // Update UI after resetting
    }

    void UpdateUI()
    {
        // Update the UI text elements with current frame, throw, and score
        if (scoreText != null)
            scoreText.text = "Score: " + totalScore;

        if (frameText != null)
            frameText.text = "Frame: " + currentFrame;

        if (throwText != null)
            throwText.text = "Throw: " + currentThrow;
    }

    void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + totalScore);
        // TODO: Handle game over logic (e.g., show final score, restart option, etc.)
        ResetGame();
    }

    void ResetGame()
    {
        currentFrame = 1;
        currentThrow = 1;
        totalScore = 0;
        pinManager.ResetPins();
        ballController.ResetBall();
        UpdateUI(); // Update UI to reflect reset state
    }
}
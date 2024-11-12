using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro


public class GameManager : MonoBehaviour
{
    public PinManager pinManager;
    public BallController ballController;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI frameText;
    public TextMeshProUGUI throwText;
    public ScoreKeeper scoreKeeper;

    private int currentFrame = 1;
    private int currentThrow = 1;
    private const int maxFrames = 10;
    private int totalScore = 0;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (ballController.BallThrown && pinManager.CheckIfRoundOver())
        {
            ProcessThrow();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // For manual testing
        {
            ProcessThrow();
        }
    }

    void ProcessThrow()
    {
        int knockedDownCount = pinManager.GetKnockedDownCount();

        // Record the roll in the ScoreKeeper
        scoreKeeper.RecordRoll(knockedDownCount);

        // Update the UI with the new score
        totalScore = scoreKeeper.GetTotalScore(); // Now works after adding GetTotalScore method
        // Update the score text in the UI once we have the UI

        // Move to the next throw/frame
        if (currentThrow == 1)
        {
            currentThrow = 2;
        }
        else
        {
            if (currentFrame < maxFrames)
            {
                currentFrame++;
                currentThrow = 1;
            }
            else
            {
                // Handle bonus rolls for the 10th frame
                if (scoreKeeper.IsGameOver())
                {
                    EndGame();
                    return;
                }
                currentThrow = 1;
            }
        }

        pinManager.ResetPins();
        ballController.ResetBall();

        UpdateUI(); // Update UI when UI is implemented
    }

    void UpdateUI()
    {
        if (frameText != null)
            frameText.text = "Frame: " + currentFrame;

        if (throwText != null)
            throwText.text = "Throw: " + currentThrow;
    }

    void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + totalScore);
        ResetGame();
    }

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
}
